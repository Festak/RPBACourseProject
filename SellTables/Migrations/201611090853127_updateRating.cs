namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRating : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropColumn("dbo.Ratings", "UserId");
            RenameColumn(table: "dbo.Ratings", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Ratings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ratings", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "UserId" });
            AlterColumn("dbo.Ratings", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Ratings", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Ratings", "UserId", c => c.Int());
            CreateIndex("dbo.Ratings", "User_Id");
        }
    }
}
