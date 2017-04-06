namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailAdres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscribes", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscribes", "UserEmail");
        }
    }
}
