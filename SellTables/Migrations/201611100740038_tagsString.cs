namespace SellTables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagsString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "TagsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "TagsString");
        }
    }
}
