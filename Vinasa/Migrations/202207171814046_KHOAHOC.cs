namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KHOAHOC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.THAMGIAKHOAHOC",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IdKhoaHoc = c.Int(),
                    HoTen = c.String(nullable:false),
                    CongTyToChucCoQuan = c.String(nullable: false),
                    ChucDanh = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    Sdt = c.String(nullable: false),
                    SoLuongHocVien = c.Int(nullable: false),
                    HoiVienVinasa = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KHOAHOC", t => t.IdKhoaHoc)
                .Index(t => t.IdKhoaHoc);

            CreateTable(
                "dbo.KHOAHOC",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MaSoThue = c.String(),
                    TenKhoaDaotao = c.String(),
                    NgayBatDau = c.DateTime(nullable: false),
                    NgayKetThuc = c.DateTime(nullable: false),
                    HinhThuc = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.THAMGIAKHOAHOC", "IdKhoaHoc", "dbo.KHOAHOC");
            DropIndex("dbo.THAMGIAKHOAHOC", new[] { "IdKhoaHoc" });
            DropTable("dbo.KHOAHOC");
            DropTable("dbo.THAMGIAKHOAHOC");
        }
    }
}
