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
    public class MemberFeePeriodControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new MemberFeePeriodController();

            var result = con.ManageFeesPeriod() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<KyPhi>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.KyPhis.Count(), model.Count);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new MemberFeePeriodController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var period = db.KyPhis.First();
            var result = con.Edit(period.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as KyPhi;
            Assert.IsNotNull(model);
            Assert.AreEqual(period.MaSoThue, model.MaSoThue);
            Assert.AreEqual(period.TenCongTy, model.TenCongTy);
            Assert.AreEqual(period.Nam, model.Nam);
            Assert.AreEqual(period.SoTienDong, model.SoTienDong);
        }
    }
}
