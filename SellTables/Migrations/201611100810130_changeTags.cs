namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Chapter_Id", "dbo.Chapters");
            DropIndex("dbo.Tags", new[] { "Chapter_Id" });
            CreateTable(
                "dbo.TagChapters",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Chapter_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Chapter_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Chapters", t => t.Chapter_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Chapter_Id);
            
            DropColumn("dbo.Tags", "Chapter_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Chapter_Id", c => c.Int());
            DropForeignKey("dbo.TagChapters", "Chapter_Id", "dbo.Chapters");
            DropForeignKey("dbo.TagChapters", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagChapters", new[] { "Chapter_Id" });
            DropIndex("dbo.TagChapters", new[] { "Tag_Id" });
            DropTable("dbo.TagChapters");
            CreateIndex("dbo.Tags", "Chapter_Id");
            AddForeignKey("dbo.Tags", "Chapter_Id", "dbo.Chapters", "Id");
        }
    }
}
