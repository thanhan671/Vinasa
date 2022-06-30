namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminars1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Seminars", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Seminars", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Seminars", "Address", c => c.String());
            AlterColumn("dbo.Seminars", "Title", c => c.String());
        }
    }
}
