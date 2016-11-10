namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTags1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Description", c => c.String(maxLength: 13));
        }
    }
}
