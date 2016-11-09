namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Creatives", "EditDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Creatives", "EditDate", c => c.DateTime(nullable: false));
        }
    }
}
