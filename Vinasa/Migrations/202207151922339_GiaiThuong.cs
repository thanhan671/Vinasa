namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiaiThuong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GiaiThuongs",
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
                "dbo.NguoiNhanGiaiThuong",
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
                .ForeignKey("dbo.GiaiThuongs", t => t.GiaiThuongId)
                .Index(t => t.GiaiThuongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NguoiNhanGiaiThuong", "GiaiThuongId", "dbo.GiaiThuongs");
            DropIndex("dbo.NguoiNhanGiaiThuong", new[] { "GiaiThuongId" });
            DropTable("dbo.NguoiNhanGiaiThuong");
            DropTable("dbo.GiaiThuongs");
        }
    }
}
