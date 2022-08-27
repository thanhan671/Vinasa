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
    public class CourseParticipantsControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new CourseParticipantsController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<THAMGIAKHOAHOC>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.THAMGIAKHOAHOCs.Count(), model.Count);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new CourseParticipantsController();

            var cource = db.THAMGIAKHOAHOCs.First();
            var result = con.Details(cource.Id) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new CourseParticipantsController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var cource = db.THAMGIAKHOAHOCs.First();
            var result = con.Edit(cource.Id) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as THAMGIAKHOAHOC;
            Assert.IsNotNull(model);
            Assert.AreEqual(cource.HoTen, model.HoTen);
            Assert.AreEqual(cource.CongTyToChucCoQuan, model.CongTyToChucCoQuan);
            Assert.AreEqual(cource.ChucDanh, model.ChucDanh);
            Assert.AreEqual(cource.Email, model.Email);
            Assert.AreEqual(cource.Sdt, model.Sdt);
            Assert.AreEqual(cource.SoLuongHocVien, model.SoLuongHocVien);
            Assert.AreEqual(cource.HoiVienVinasa, model.HoiVienVinasa);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new CourseParticipantsController();

            var cource = db.THAMGIAKHOAHOCs.First();
            var result = con.DeleteSelected(cource.Id) as PartialViewResult;

            Assert.IsNotNull(result);
        }
    }
}
