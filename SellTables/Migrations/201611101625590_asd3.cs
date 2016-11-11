namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedalApplicationUsers", "Medal_Id", "dbo.Medals");
            DropForeignKey("dbo.MedalApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MedalApplicationUsers", new[] { "Medal_Id" });
            DropIndex("dbo.MedalApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Medals", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Medals", "ApplicationUser_Id");
            AddForeignKey("dbo.Medals", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.MedalApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedalApplicationUsers",
                c => new
                    {
                        Medal_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Medal_Id, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.Medals", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Medals", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Medals", "ApplicationUser_Id");
            CreateIndex("dbo.MedalApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.MedalApplicationUsers", "Medal_Id");
            AddForeignKey("dbo.MedalApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedalApplicationUsers", "Medal_Id", "dbo.Medals", "Id", cascadeDelete: true);
        }
    }
}
