namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Chapters", name: "Creative_Id", newName: "CreativeId");
            RenameIndex(table: "dbo.Chapters", name: "IX_Creative_Id", newName: "IX_CreativeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Chapters", name: "IX_CreativeId", newName: "IX_Creative_Id");
            RenameColumn(table: "dbo.Chapters", name: "CreativeId", newName: "Creative_Id");
        }
    }
}
