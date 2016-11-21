﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hale.Core.Models.Messages
{
    public class LoginAttempt
    {
        /// <summary>
        /// Username provided from a frontend login attempt
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password in plain text provided from a frontend login attempt
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Persistancy toggle provided from a frontend login attempt
        /// </summary>
        public bool Persistent { get; set; }
    }
}
