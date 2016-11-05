namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateOfCreation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creatives", "CreationDate");
        }
    }
}
