namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seminars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeminarParticipants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeminarId = c.Int(),
                        Name = c.String(),
                        TaxNumber = c.String(),
                        Company = c.String(),
                        Position = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        ProvinceId = c.Int(nullable: false),
                        JobTitle = c.String(),
                        Operation = c.String(),
                        RegistrySeminar = c.Boolean(nullable: false),
                        RegistryBusinessMatching = c.Boolean(nullable: false),
                        RegistryExhibition = c.Boolean(nullable: false),
                        RegistryTicket = c.Boolean(nullable: false),
                        CreatedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seminars", t => t.SeminarId)
                .Index(t => t.SeminarId);
            
            CreateTable(
                "dbo.Seminars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        OpenDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        CreatedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars");
            DropIndex("dbo.SeminarParticipants", new[] { "SeminarId" });
            DropTable("dbo.Seminars");
            DropTable("dbo.SeminarParticipants");
            DropTable("dbo.Provinces");
        }
    }
}
