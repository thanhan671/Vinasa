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
    public class ICParticipantsControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new ICParticipantsController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<THAMGIAHOINGHIQUOCTE>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.THAMGIAHOINGHIQUOCTEs.Count(), model.Count);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new ICParticipantsController();

            var cource = db.THAMGIAHOINGHIQUOCTEs.First();
            var result = con.Details(cource.ID) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new ICParticipantsController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var cource = db.THAMGIAHOINGHIQUOCTEs.First();
            var result = con.Edit(cource.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as THAMGIAHOINGHIQUOCTE;
            Assert.IsNotNull(model);
            Assert.AreEqual(cource.TenDonVi, model.TenDonVi);
            Assert.AreEqual(cource.DonViChungToiLa, model.DonViChungToiLa);
            Assert.AreEqual(cource.DiaChi, model.DiaChi);
            Assert.AreEqual(cource.DienThoai, model.DienThoai);
            Assert.AreEqual(cource.DaiDienLienHe, model.DaiDienLienHe);
            Assert.AreEqual(cource.ChucVu, model.ChucVu);
            Assert.AreEqual(cource.DiDong, model.DiDong);
            Assert.AreEqual(cource.DonViChungToiLa, model.DonViChungToiLa);
            Assert.AreEqual(cource.Email, model.Email);
            Assert.AreEqual(cource.DangKyThamDu, model.DangKyThamDu);
            Assert.AreEqual(cource.DangKyPhatBieu, model.DangKyPhatBieu);
            Assert.AreEqual(cource.DangKyGianHangTrienLam, model.DangKyGianHangTrienLam);
            Assert.AreEqual(cource.DangKyBusinessMatchingOnline, model.DangKyBusinessMatchingOnline);
            Assert.AreEqual(cource.DangKyBusinessMatchingOffline, model.DangKyBusinessMatchingOffline);
            Assert.AreEqual(cource.DangKyTaiTro, model.DangKyTaiTro);
            Assert.AreEqual(cource.DangKyQuangCao, model.DangKyQuangCao);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new ICParticipantsController();

            var cource = db.THAMGIAHOINGHIQUOCTEs.First();
            var result = con.DeleteSelected(cource.ID) as PartialViewResult;

            Assert.IsNotNull(result);
        }

    }
}
