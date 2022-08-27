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
    public class NGUOINHANGIAITHUONGControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new NGUOINHANGIAITHUONGController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<NGUOINHANGIAITHUONG>;
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void DetailTest()
        {

            var con = new NGUOINHANGIAITHUONGController();

            var result = con.Details(0) as ViewResult;

            Assert.IsNull(result);
        }
    }
}

