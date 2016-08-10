namespace Hale.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ModifiedBy", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Changed");
            DropColumn("dbo.Users", "ChangedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ChangedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Changed", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "ModifiedBy");
            DropColumn("dbo.Users", "Modified");
            DropColumn("dbo.Users", "Password");
        }
    }
}