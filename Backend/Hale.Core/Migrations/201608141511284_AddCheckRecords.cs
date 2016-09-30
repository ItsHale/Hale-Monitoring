namespace Hale.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCheckRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Modules.CheckRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.Double(nullable: false),
                        Result_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Modules.Results", t => t.Result_Id)
                .Index(t => t.Result_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Modules.CheckRecords", "Result_Id", "Modules.Results");
            DropIndex("Modules.CheckRecords", new[] { "Result_Id" });
            DropTable("Modules.CheckRecords");
        }
    }
}