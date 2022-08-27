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
    public class MemberPayFeesControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new MemberPayFeesController();

            var result = con.ManagePayFees() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<DongPhi>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.DongPhis.Count(), model.Count);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new MemberPayFeesController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var payFee = db.DongPhis.First();
            var result = con.Edit(payFee.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as DongPhi;
            Assert.IsNotNull(model);
            Assert.AreEqual(payFee.MaSoThue, model.MaSoThue);
            Assert.AreEqual(payFee.TenCongTy, model.TenCongTy);
            Assert.AreEqual(payFee.DiaChiGhiPhieuThu, model.DiaChiGhiPhieuThu);
            Assert.AreEqual(payFee.DiaChiGuiPhieuThu, model.DiaChiGuiPhieuThu);
            Assert.AreEqual(payFee.NguoiNhanPhieuThu, model.NguoiNhanPhieuThu);
            Assert.AreEqual(payFee.Sdt, model.Sdt);
            Assert.AreEqual(payFee.NgayChuyenTien, model.NgayChuyenTien);
            Assert.AreEqual(payFee.NgayGuiPhieuThu, model.NgayGuiPhieuThu);
            Assert.AreEqual(payFee.SoTienDong, model.SoTienDong);
            Assert.AreEqual(payFee.GhiChu, model.GhiChu);

        }

    }
}
