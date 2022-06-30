namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminar4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars");
            DropIndex("dbo.SeminarParticipants", new[] { "SeminarId" });
            AlterColumn("dbo.SeminarParticipants", "SeminarId", c => c.Int());
            CreateIndex("dbo.SeminarParticipants", "SeminarId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars");
            DropIndex("dbo.SeminarParticipants", new[] { "SeminarId" });
            AlterColumn("dbo.SeminarParticipants", "SeminarId", c => c.Int(nullable: false));
            CreateIndex("dbo.SeminarParticipants", "SeminarId");
            AddForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars", "Id", cascadeDelete: true);
        }
    }
}
