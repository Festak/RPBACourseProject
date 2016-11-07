namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class virtualReturn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives");
            DropForeignKey("dbo.Ratings", "ChapterId", "dbo.Chapters");
            DropIndex("dbo.Chapters", new[] { "CreativeId" });
            DropIndex("dbo.Ratings", new[] { "ChapterId" });
            AlterColumn("dbo.Chapters", "CreativeId", c => c.Int());
            AlterColumn("dbo.Ratings", "ChapterId", c => c.Int());
            CreateIndex("dbo.Chapters", "CreativeId");
            CreateIndex("dbo.Ratings", "ChapterId");
            AddForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives", "Id");
            AddForeignKey("dbo.Ratings", "ChapterId", "dbo.Chapters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "ChapterId", "dbo.Chapters");
            DropForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Ratings", new[] { "ChapterId" });
            DropIndex("dbo.Chapters", new[] { "CreativeId" });
            AlterColumn("dbo.Ratings", "ChapterId", c => c.Int(nullable: false));
            AlterColumn("dbo.Chapters", "CreativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "ChapterId");
            CreateIndex("dbo.Chapters", "CreativeId");
            AddForeignKey("dbo.Ratings", "ChapterId", "dbo.Chapters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives", "Id", cascadeDelete: true);
        }
    }
}
