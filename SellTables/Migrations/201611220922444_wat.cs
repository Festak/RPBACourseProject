namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wat : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Creatives", "CreativeUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Creatives", "CreativeUri", c => c.String());
        }
    }
}
