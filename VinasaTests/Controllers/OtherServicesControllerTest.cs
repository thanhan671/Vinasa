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
    public class OtherServicesControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var con = new OtherServicesController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SUDUNGDICHVUKHAC>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.SUDUNGDICHVUKHACs.Count(), model.Count);
        }
        [TestMethod]
        public void DetailTest()
        {
            var ran = new Random();
            var produc = new SUDUNGDICHVUKHAC()
            {
                ID = ran.Next()
            };

            var con = new OtherServicesController();
            var re = con.Details(produc.ID) as ViewResult;

            Assert.IsNull(re);

        }
        [TestMethod]
        public void EditTest()
        {
            var con = new OtherServicesController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var otherSevices = db.SUDUNGDICHVUKHACs.First();
            var result = con.Edit(otherSevices.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as SUDUNGDICHVUKHAC;
            Assert.IsNotNull(model);
            Assert.AreEqual(otherSevices.MaSoThue, model.MaSoThue);
            Assert.AreEqual(otherSevices.TenCongTy, model.TenCongTy);
            Assert.AreEqual(otherSevices.NgayBatDauHopDong, model.NgayBatDauHopDong);
            Assert.AreEqual(otherSevices.NgayKetThucHopDong, model.NgayKetThucHopDong);
            Assert.AreEqual(otherSevices.TenDichVuKhac, model.TenDichVuKhac);
            Assert.AreEqual(otherSevices.GiaGoc, model.GiaGoc);
            Assert.AreEqual(otherSevices.GiaUuDai, model.GiaUuDai);
            Assert.AreEqual(otherSevices.ChiecKhauVinasa, model.ChiecKhauVinasa);
            Assert.AreEqual(otherSevices.TenNguoiLienHe, model.TenNguoiLienHe);
            Assert.AreEqual(otherSevices.ChucDanh, model.ChucDanh);
            Assert.AreEqual(otherSevices.Email, model.Email);
            Assert.AreEqual(otherSevices.DienThoai, model.DienThoai);
            Assert.AreEqual(otherSevices.GhiChu, model.GhiChu);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new OtherServicesController();

            var otherSevices = db.SUDUNGDICHVUKHACs.First();
            var result = con.DeleteSelected(otherSevices.ID) as PartialViewResult;

            Assert.IsNotNull(result);
        }

    }
}
