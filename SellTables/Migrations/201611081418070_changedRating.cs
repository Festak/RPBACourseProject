namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "ChapterId", "dbo.Chapters");
            DropIndex("dbo.Ratings", new[] { "ChapterId" });
            RenameColumn(table: "dbo.Ratings", name: "Creative_Id", newName: "CreativeId");
            RenameIndex(table: "dbo.Ratings", name: "IX_Creative_Id", newName: "IX_CreativeId");
            DropColumn("dbo.Chapters", "IsReading");
            DropColumn("dbo.Ratings", "ChapterId");
            DropColumn("dbo.Medals", "IsSelected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medals", "IsSelected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ratings", "ChapterId", c => c.Int());
            AddColumn("dbo.Chapters", "IsReading", c => c.Boolean(nullable: false));
            RenameIndex(table: "dbo.Ratings", name: "IX_CreativeId", newName: "IX_Creative_Id");
            RenameColumn(table: "dbo.Ratings", name: "CreativeId", newName: "Creative_Id");
            CreateIndex("dbo.Ratings", "ChapterId");
            AddForeignKey("dbo.Ratings", "ChapterId", "dbo.Chapters", "Id");
        }
    }
}
