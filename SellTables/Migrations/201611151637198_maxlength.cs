namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Chapters", "Name", c => c.String(maxLength: 20));
            AlterColumn("dbo.Creatives", "Name", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Creatives", "Name", c => c.String());
            AlterColumn("dbo.Chapters", "Name", c => c.String());
        }
    }
}
