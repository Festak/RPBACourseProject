namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newone : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Creatives", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.Creatives", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Creatives", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Creatives", name: "CategoryId", newName: "Category_Id");
        }
    }
}
