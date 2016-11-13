namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMdal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medals", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Medals", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.MedalApplicationUsers",
                c => new
                    {
                        Medal_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Medal_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Medals", t => t.Medal_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Medal_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Medals", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medals", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.MedalApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MedalApplicationUsers", "Medal_Id", "dbo.Medals");
            DropIndex("dbo.MedalApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MedalApplicationUsers", new[] { "Medal_Id" });
            DropTable("dbo.MedalApplicationUsers");
            CreateIndex("dbo.Medals", "ApplicationUser_Id");
            AddForeignKey("dbo.Medals", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
