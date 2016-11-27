namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingchange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ratings", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "test", c => c.Int(nullable: false));
        }
    }
}
