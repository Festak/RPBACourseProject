namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDateChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Creatives", "EditDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Creatives", "EditDate", c => c.DateTime());
        }
    }
}
