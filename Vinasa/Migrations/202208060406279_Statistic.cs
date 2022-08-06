namespace Vinasa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Statistic : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HoiPhi", "NgayChuyenTien", c => c.DateTime());
            AlterColumn("dbo.HoiPhi", "NgayGuiPhieuThu", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HoiPhi", "NgayGuiPhieuThu", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HoiPhi", "NgayChuyenTien", c => c.DateTime(nullable: false));
        }
    }
}
