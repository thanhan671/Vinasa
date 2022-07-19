namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GIAITHUONG",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        OpenDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        CreatedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NGUOINHANGIAITHUONG",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiaiThuongId = c.Int(),
                        MaSoThue = c.String(nullable: false),
                        TenDonVi = c.String(nullable: false),
                        TenNguoiDaiDienPhapLuat = c.String(nullable: false),
                        ChucDanh = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DiDong = c.String(nullable: false),
                        TenNguoiLienHeVoiBTC = c.String(nullable: false),
                        ChucDanhNguoiLienHe = c.String(nullable: false),
                        EmailNguoiLienHe = c.String(nullable: false),
                        DiDongNguoiLienHe = c.String(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        DiaChi = c.String(nullable: false),
                        PhieuDangKy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GIAITHUONG", t => t.GiaiThuongId)
                .Index(t => t.GiaiThuongId);
            
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
                        Name = c.String(nullable: false),
                        TaxNumber = c.String(nullable: false),
                        Company = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        JobTitle = c.String(nullable: false),
                        Operation = c.String(nullable: false),
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
                        Title = c.String(nullable: false),
                        OpenDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        CreatedUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeminarParticipants", "SeminarId", "dbo.Seminars");
            DropForeignKey("dbo.NGUOINHANGIAITHUONG", "GiaiThuongId", "dbo.GIAITHUONG");
            DropIndex("dbo.SeminarParticipants", new[] { "SeminarId" });
            DropIndex("dbo.NGUOINHANGIAITHUONG", new[] { "GiaiThuongId" });
            DropTable("dbo.Seminars");
            DropTable("dbo.SeminarParticipants");
            DropTable("dbo.Provinces");
            DropTable("dbo.NGUOINHANGIAITHUONG");
            DropTable("dbo.GIAITHUONG");
        }
    }
}
