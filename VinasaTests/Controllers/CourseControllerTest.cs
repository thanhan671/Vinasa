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
    public class CourseControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new CourseController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<KHOAHOC>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.KHOAHOCs.Count(), model.Count);
        }
        [TestMethod]
        public void CreateTest()
        {

            var con = new CourseController();

            var result = con.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new CourseController();

            var cource = db.KHOAHOCs.First();
            var result = con.Details(cource.Id) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new CourseController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var cource = db.KHOAHOCs.First();
            var result = con.Edit(cource.Id) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as KHOAHOC;
            Assert.IsNotNull(model);
            Assert.AreEqual(cource.TenKhoaDaoTao, model.TenKhoaDaoTao);
            Assert.AreEqual(cource.NgayBatDau, model.NgayBatDau);
            Assert.AreEqual(cource.NgayKetThuc, model.NgayKetThuc);
            Assert.AreEqual(cource.HinhThuc, model.HinhThuc);
            Assert.AreEqual(cource.TenGiangVien, model.TenGiangVien);
            Assert.AreEqual(cource.DiaDiem, model.DiaDiem);
            Assert.AreEqual(cource.HocPhi, model.HocPhi);
        }
        [TestMethod]
        public void DeletelTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new CourseController();

            var cource = db.KHOAHOCs.First();
            var result = con.DeleteSelected(cource.Id) as PartialViewResult;

            Assert.IsNotNull(result);
        }
    }
}
