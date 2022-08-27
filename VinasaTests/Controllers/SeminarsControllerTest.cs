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
    public class SeminarsControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new SeminarsController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<Seminar>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.Seminars.Count(), model.Count);
        }
        [TestMethod]
        public void CreateTest()
        {

            var con = new SeminarsController();

            var result = con.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new SeminarsController();

            var ic = db.Seminars.First();
            var result = con.Details(ic.Id) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new SeminarsController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var semi = db.Seminars.First();
            var result = con.Edit(semi.Id) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as Seminar;
            Assert.IsNotNull(model);
            Assert.AreEqual(semi.Title, model.Title);
            Assert.AreEqual(semi.OpenDate, model.OpenDate);
            Assert.AreEqual(semi.CloseDate, model.CloseDate);
            Assert.AreEqual(semi.Address, model.Address);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new SeminarsController();

            var ic = db.Seminars.First();
            var result = con.DeleteSelected(ic.Id) as PartialViewResult;

            Assert.IsNotNull(result);
        }

    }
}
