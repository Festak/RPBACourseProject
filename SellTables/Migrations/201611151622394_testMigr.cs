namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMigr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "test", c => c.Int(nullable: false));
            AlterColumn("dbo.Chapters", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Chapters", "Name", c => c.String(maxLength: 50));
            DropColumn("dbo.Ratings", "test");
        }
    }
}
