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
    public class ICControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new ICController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<HOINGHIQUOCTE>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.HOINGHIQUOCTEs.Count(), model.Count);
        }
        [TestMethod]
        public void CreateTest()
        {

            var con = new ICController();

            var result = con.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new ICController();

            var ic = db.HOINGHIQUOCTEs.First();
            var result = con.Details(ic.ID) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new ICController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var ic = db.HOINGHIQUOCTEs.First();
            var result = con.Edit(ic.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as HOINGHIQUOCTE;
            Assert.IsNotNull(model);
            Assert.AreEqual(ic.Ten, model.Ten);
            Assert.AreEqual(ic.DiaDiem, model.DiaDiem);
            Assert.AreEqual(ic.ThoiGianBatDau, model.ThoiGianBatDau);
            Assert.AreEqual(ic.ThoiGianKetThuc, model.ThoiGianKetThuc);
        }
        [TestMethod]
        public void DeletelTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new ICController();

            var ic = db.HOINGHIQUOCTEs.First();
            var result = con.DeleteSelected(ic.ID) as PartialViewResult;

            Assert.IsNotNull(result);
        }
    }
}
