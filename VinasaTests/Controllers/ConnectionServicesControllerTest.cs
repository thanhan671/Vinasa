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
    public class ConnectionServicesControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var con = new ConnectionServicesController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SUDUNGDICHVUKETNOI>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.SUDUNGDICHVUKETNOIs.Count(), model.Count);
        }
        [TestMethod]
        public void DetailTest()
        {
            var ran = new Random();
            var produc = new SUDUNGDICHVUKETNOI()
            {
                ID = ran.Next()
            };

            var con = new ConnectionServicesController();
            var re = con.Details(produc.ID) as ViewResult;

            Assert.IsNull(re);

        }
        [TestMethod]
        public void EditTest()
        {
            var con = new ConnectionServicesController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var connectSevices = db.SUDUNGDICHVUKETNOIs.First();
            var result = con.Edit(connectSevices.ID) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as SUDUNGDICHVUKETNOI;
            Assert.IsNotNull(model);
            Assert.AreEqual(connectSevices.MaSoThue, model.MaSoThue);
            Assert.AreEqual(connectSevices.TenCongTy, model.TenCongTy);
            Assert.AreEqual(connectSevices.NgayBatDauHopDong, model.NgayBatDauHopDong);
            Assert.AreEqual(connectSevices.NgayKetThucHopDong, model.NgayKetThucHopDong);
            Assert.AreEqual(connectSevices.TenDichVu, model.TenDichVu);
            Assert.AreEqual(connectSevices.GiaGoc, model.GiaGoc);
            Assert.AreEqual(connectSevices.GiaUuDai, model.GiaUuDai);
            Assert.AreEqual(connectSevices.ChiecKhauVinasa, model.ChiecKhauVinasa);
            Assert.AreEqual(connectSevices.TenNguoiLienHe, model.TenNguoiLienHe);
            Assert.AreEqual(connectSevices.ChucDanh, model.ChucDanh);
            Assert.AreEqual(connectSevices.Email, model.Email);
            Assert.AreEqual(connectSevices.DienThoai, model.DienThoai);
            Assert.AreEqual(connectSevices.GhiChu, model.GhiChu);
        }
        [TestMethod]
        public void DeletelTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new ConnectionServicesController();

            var connectSevices = db.SUDUNGDICHVUKETNOIs.First();
            var result = con.Delete(connectSevices.ID) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
