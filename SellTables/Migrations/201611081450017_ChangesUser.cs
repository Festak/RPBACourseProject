namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "UserId", c => c.Int());
            AddColumn("dbo.Ratings", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "AvatarUri", c => c.String());
            AddColumn("dbo.AspNetUsers", "Language", c => c.String());
            AddColumn("dbo.AspNetUsers", "Theme", c => c.String());
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Ratings", "User_Id");
            AddForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "AvararUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AvararUri", c => c.String());
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
            DropColumn("dbo.AspNetUsers", "Theme");
            DropColumn("dbo.AspNetUsers", "Language");
            DropColumn("dbo.AspNetUsers", "AvatarUri");
            DropColumn("dbo.Ratings", "User_Id");
            DropColumn("dbo.Ratings", "UserId");
        }
    }
}
