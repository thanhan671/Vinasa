namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminar2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars");
            DropIndex("dbo.SeminarParticipants", new[] { "SeminarId" });
            AlterColumn("dbo.SeminarParticipants", "SeminarId", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropIndex("dbo.SeminarParticipants", new[] { "SeminarId" });
        }
    }
}
