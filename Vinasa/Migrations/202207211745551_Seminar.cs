namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seminars", "CloseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seminars", "CloseDate");
        }
    }
}
