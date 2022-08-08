namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiaiThuong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NGUOINHANGIAITHUONG", "KinhPhiTruyenThong", c => c.Int(nullable: false));
            AddColumn("dbo.NGUOINHANGIAITHUONG", "GiaiThuong", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NGUOINHANGIAITHUONG", "GiaiThuong");
            DropColumn("dbo.NGUOINHANGIAITHUONG", "KinhPhiTruyenThong");
        }
    }
}
