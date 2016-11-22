namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "CreativeUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creatives", "CreativeUri");
        }
    }
}
