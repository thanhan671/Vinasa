namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminars2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeminarParticipants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "TaxNumber", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "Company", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "JobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.SeminarParticipants", "Operation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeminarParticipants", "Operation", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "JobTitle", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "PhoneNumber", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "Email", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "Position", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "Company", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "TaxNumber", c => c.String());
            AlterColumn("dbo.SeminarParticipants", "Name", c => c.String());
        }
    }
}
