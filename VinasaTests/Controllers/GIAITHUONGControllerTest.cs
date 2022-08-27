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
    public class GIAITHUONGControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new GIAITHUONGController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<GIAITHUONG>;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CreateTest()
        {

            var con = new GIAITHUONGController();

            var result = con.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new GIAITHUONGController();

            var result = con.Details(1) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new GIAITHUONGController();

            var result = con.DeleteSelected(1) as PartialViewResult;

            Assert.IsNotNull(result);
        }
    }
}
