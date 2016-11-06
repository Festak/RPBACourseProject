namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Chapters", new[] { "CreativeId" });
            AlterColumn("dbo.Chapters", "CreativeId", c => c.Int());
            CreateIndex("dbo.Chapters", "CreativeId");
            AddForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Chapters", new[] { "CreativeId" });
            AlterColumn("dbo.Chapters", "CreativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Chapters", "CreativeId");
            AddForeignKey("dbo.Chapters", "CreativeId", "dbo.Creatives", "Id", cascadeDelete: true);
        }
    }
}
