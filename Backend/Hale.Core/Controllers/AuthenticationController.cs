﻿using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Hale.Core.Models;
using Hale.Core.Models.Users;
using Hale.Core.Handlers;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using NLog;
using Microsoft.Owin;
using Hale.Core.Models.Shared;
using Hale.Core.Contexts;
using System.Linq;
using System;

namespace Hale.Core.Controllers
{
    /// <summary>
    /// API for handling logins, logouts and status checks.
    /// </summary>
    [RoutePrefix("api/v1/authentication")]
    public class AuthenticationController : ApiController
    {

        private readonly Logger      _log;
        private readonly HaleDBContext _db;

        // readonly Users _users;


        internal AuthenticationController() : this(new HaleDBContext())
        {
        }

        /// <summary>
        /// Constructor for AuthenticationController
        /// </summary>
        /// <param name="context"></param>
        public AuthenticationController(HaleDBContext context)
        {
            _log = LogManager.GetCurrentClassLogger();
            _db = context;
        }

        private LoginResponse DoLogin(string username, string password, bool persistent = false)
        {
            try
            {
                var user = _db.Accounts.FirstOrDefault(x => x.UserName == username);

                if (user == null) return LoginResponse.InvalidCredentialsResponse();

                var passwordAccepted = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (passwordAccepted)
                {
                    var context = Request.GetOwinContext();
                    context.Authentication.SignIn(
                        new AuthenticationProperties()
                        {
                            IsPersistent = persistent,
                        },
                        new ClaimsIdentity(
                            new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, username) },
                            "HaleCoreAuth"
                        )
                    );

                    return new LoginResponse()
                    {
                        UserId = user.Id,
                        Error = ""
                    };
                }
                else if (user.PasswordChanged.HasValue && BCrypt.Net.BCrypt.Verify(password, user.OldPassword))
                {
                    return LoginResponse.PasswordChangedResponse(user.PasswordChanged.Value);
                }
                else
                    return LoginResponse.InvalidCredentialsResponse();
            }
            catch(Exception x)
            {
                _log.Warn(x, $"Error authenticating from {Request.GetOwinContext().Request.RemoteIpAddress}, user \"{username}\": {x.Message}");
#if DEBUG
                return LoginResponse.ErrorResponse(x.Message);
#else
                return LoginResponse.ErrorResponse("Internal error");
#endif
            }

        }


        /// <summary>
        /// Creates claim and session if the authentication succeeds. 
        /// </summary>
        /// <param name="auth">A JSON Serialized authentication attempt.</param>
        /// <returns>A custom LoginResponse that will be stored in the local storage for the Hale-GUI ember application.</returns>
        [Route("login")]
        [HttpPost]
        public LoginResponse Login([FromBody]Authentication auth)
        {
            _log.Info(JsonConvert.SerializeObject(auth));
            return DoLogin(auth.Username, auth.Password);
        }


        /// <summary>
        /// Check whether the current session is authenticated or not.
        /// </summary>
        /// <returns>A statuscode and an error message, in case there happens to be one.</returns>
        [Route("status")]
        [Route()]
        [HttpGet]
        public HttpResponseMessage Status()
        {
            IOwinContext context = Request.GetOwinContext();
            bool authenticated = (context.Authentication.User != null);

            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    error = "",
                    authenticated = authenticated
                })),
                StatusCode = (authenticated ? HttpStatusCode.OK : HttpStatusCode.Unauthorized)

            };
        }


        /// <summary>
        /// Deletes claim and session if there is any. 
        /// </summary>
        /// <returns>200</returns>
        [Route()]
        [HttpDelete]
        public HttpResponseMessage Logout()
        {
            var context = Request.GetOwinContext();
            context.Authentication.SignOut();
            return new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    error = "",
                    authenticated = false
                })),
                StatusCode = HttpStatusCode.OK

            };
        }
    }
}
