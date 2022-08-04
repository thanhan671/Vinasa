using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vinasa.Services;
using Vinasa.Models;
using OfficeOpenXml;
using System.Data.Entity.Validation;

namespace Vinasa.Controllers
{
    public class ConnectionServicesController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        // GET: ConnectionServices
        public ActionResult Index()
        {
            return View(_db.SUDUNGDICHVUKETNOIs.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUDUNGDICHVUKETNOI usingConnectionServices = _db.SUDUNGDICHVUKETNOIs.Find(id);
            if (usingConnectionServices == null)
            {
                return HttpNotFound();
            }
            return View(usingConnectionServices);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUDUNGDICHVUKETNOI usingConnectionServices = _db.SUDUNGDICHVUKETNOIs.Find(id);

            if (usingConnectionServices == null)
            {
                return HttpNotFound();
            }

            return View(usingConnectionServices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, MaSoThue, TenCongTy, NgayBatDauHopDong, NgayKetThucHopDong, TenDichVu, GiaGoc, GiaUuDai, ChiecKhauVinasa, TenNguoiLienHe, ChucDanh, Email, DienThoai, GhiChu")] SUDUNGDICHVUKETNOI usingConnectionServices)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(usingConnectionServices).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usingConnectionServices);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            SUDUNGDICHVUKETNOI usingConnectionServices = _db.SUDUNGDICHVUKETNOIs.Find(id);

            _db.SUDUNGDICHVUKETNOIs.Remove(usingConnectionServices);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}