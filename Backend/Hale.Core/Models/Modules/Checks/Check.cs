﻿using Hale.Core.Models.Modules;
using System;

namespace Hale.Core.Models.Checks
{
    /// <summary>
    /// Corresponds to the database table Checks.Check
    /// </summary>
    public class Check: Function
    {
        /// <summary>
        /// Corresponds to the table column Checks.Check.Id
        /// </summary>
        public new int Id { get; set; }

        /// <summary>
        /// Corresponds to the table column Checks.Check.Identifier
        /// </summary>
        public string Identifier { get; set; }

    }
}