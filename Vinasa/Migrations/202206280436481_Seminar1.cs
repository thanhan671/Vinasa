namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminar1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars");
        }

        public override void Down()
        {
        }
    }
}
