﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hale.Migrations
{
    [Migration(1)]
    public class M001CreateUsersSchema : Migration
    {

        public override void Up()
        {
            Create.Schema("Users");
        }

        public override void Down()
        {
            Delete.Schema("Users");
        }
    }
}