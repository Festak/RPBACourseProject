namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCatSubs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Creatives", "Category_Id", c => c.Int());
            CreateIndex("dbo.Creatives", "Category_Id");
            AddForeignKey("dbo.Creatives", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscribes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscribes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Creatives", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Subscribes", new[] { "UserId" });
            DropIndex("dbo.Subscribes", new[] { "CategoryId" });
            DropIndex("dbo.Creatives", new[] { "Category_Id" });
            DropColumn("dbo.Creatives", "Category_Id");
            DropTable("dbo.Subscribes");
            DropTable("dbo.Categories");
        }
    }
}
