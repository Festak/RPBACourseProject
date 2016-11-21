namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Language");
            DropColumn("dbo.AspNetUsers", "Theme");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Theme", c => c.String());
            AddColumn("dbo.AspNetUsers", "Language", c => c.String());
        }
    }
}
