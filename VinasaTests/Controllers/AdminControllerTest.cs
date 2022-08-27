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
    public class AdminControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new AdminController();

            var result = con.ManageAccount() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<TAIKHOANADMIN>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.TAIKHOANADMINs.Count(), model.Count);
        }
        [TestMethod]
        public void CreateTest()
        {

            var con = new AdminController();

            var result = con.CreateAccount() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
