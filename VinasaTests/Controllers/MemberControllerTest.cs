using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Controllers;
using Vinasa.Models;

namespace VinasaTests.Controllers
{
    [TestClass]
    public class MemberControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new MemberController();

            var result = con.ManageMember() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<HOIVIEN>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.HOIVIENs.Count(), model.Count);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new MemberController();

            var ic = db.HOIVIENs.First();
            var result = con.DetailMember(ic.ID) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new MemberController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var ic = db.HOIVIENs.First();
            var result = con.Edit(ic.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as HOIVIEN;
            Assert.IsNotNull(model);
            Assert.AreEqual(ic.MaSoThue, model.MaSoThue);
            Assert.AreEqual(ic.TenTiengViet, model.TenTiengViet);
            Assert.AreEqual(ic.TenTiengAnh, model.TenTiengAnh);
            Assert.AreEqual(ic.TenVietTat, model.TenVietTat);
            Assert.AreEqual(ic.NgayThanhLap, model.NgayThanhLap);
            Assert.AreEqual(ic.Website, model.Website);
            Assert.AreEqual(ic.SdtCongTy, model.SdtCongTy);
            Assert.AreEqual(ic.EmailCongTy, model.EmailCongTy);
            Assert.AreEqual(ic.DiaChiGiaoDich, model.DiaChiGiaoDich);
            Assert.AreEqual(ic.DiaChiTrenChungTu, model.DiaChiTrenChungTu);
            Assert.AreEqual(ic.SoLuongNhanVien, model.SoLuongNhanVien);
            Assert.AreEqual(ic.SoLuongLapTrinhVien, model.SoLuongLapTrinhVien);
            Assert.AreEqual(ic.ThiTruongNoiDia, model.ThiTruongNoiDia);
            Assert.AreEqual(ic.ThiTruongQuocTe, model.ThiTruongQuocTe);
            Assert.AreEqual(ic.LinhVucHoatDong, model.LinhVucHoatDong);
            Assert.AreEqual(ic.LanhDao, model.LanhDao);
            Assert.AreEqual(ic.ChucDanhLanhDao, model.ChucDanhLanhDao);
            Assert.AreEqual(ic.SdtLanhDao, model.SdtLanhDao);
            Assert.AreEqual(ic.EmailLanhDao, model.EmailLanhDao);
            Assert.AreEqual(ic.DaiDienMarketing, model.DaiDienMarketing);
            Assert.AreEqual(ic.ChucNangMarketing, model.ChucNangMarketing);
            Assert.AreEqual(ic.SdtMarketing, model.SdtMarketing);
            Assert.AreEqual(ic.EmailMarketing, model.EmailMarketing);
            Assert.AreEqual(ic.DaiDienNhanSu, model.DaiDienNhanSu);
            Assert.AreEqual(ic.ChucDanhNhanSu, model.ChucDanhNhanSu);
            Assert.AreEqual(ic.SdtNhanSu, model.SdtNhanSu);
            Assert.AreEqual(ic.EmailNhanSu, model.EmailNhanSu);
            Assert.AreEqual(ic.DaiDienKeToan, model.DaiDienKeToan);
            Assert.AreEqual(ic.ChucDanhKeToan, model.ChucDanhKeToan);
            Assert.AreEqual(ic.SdtKeToan, model.SdtKeToan);
            Assert.AreEqual(ic.EmailKeToan, model.EmailKeToan);
            Assert.AreEqual(ic.Fanpage, model.Fanpage);
            Assert.AreEqual(ic.ThoiGianGiaNhap, model.ThoiGianGiaNhap);
            Assert.AreEqual(ic.KhuVuc, model.KhuVuc);
            Assert.AreEqual(ic.GhiChu, model.GhiChu);
        }
    }
}
