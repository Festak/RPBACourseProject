namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnOfDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creatives", "Name");
        }
    }
}
