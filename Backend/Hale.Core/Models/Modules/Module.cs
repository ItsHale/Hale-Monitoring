﻿using Hale.Lib.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hale.Core.Models.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public class Module
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public Version Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Major {
            set { updateVersion( major: value ); }
            get { return Version.Major; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Minor {
            set { updateVersion( minor: value ); }
            get { return Version.Minor; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Revision {
            set { updateVersion( revision: value ); }
            get { return Version.Revision; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int? _major, _minor, _revision;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="revision"></param>
        private void updateVersion (int? major = null, int? minor = null, int? revision = null)
        {
            if (major.HasValue) _major = major;
            if (minor.HasValue) _minor = minor;
            if (revision.HasValue) _revision = revision;

            if (_major.HasValue && _minor.HasValue && _revision.HasValue)
                Version = new Version(_major.Value, _minor.Value, 0, _revision.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Module() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vi"></param>
        public Module(VersionedIdentifier vi)
        {
            Identifier = vi.Identifier;
            Version = vi.Version;
        }
    }
}
