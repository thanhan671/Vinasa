using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vinasa.Services;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class ICParticipantsController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        public ActionResult Index()
        {
            return View(_db.THAMGIAHOINGHIQUOCTEs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAHOINGHIQUOCTE ICParticipants = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            if (ICParticipants == null)
            {
                return HttpNotFound();
            }
            ICParticipants.HOINGHIQUOCTE = _db.HOINGHIQUOCTEs.Find(ICParticipants.HoiNghiQT_ID);
            return View(ICParticipants);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAHOINGHIQUOCTE ICParticipants = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            if (ICParticipants == null)
            {
                return HttpNotFound();
            }

            return View(ICParticipants);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, TenDonVi, DonViChungToiLa, DiaChi, DienThoai, DaiDienLienHe, ChucVu, DiDong, " +
            "Email, DangKyThamDu, DangKyPhatBieu, DangKyGianHangTrienLam, DangKyBusinessMatchingOnline, DangKyBusinessMatchingOffline, DangKyTaiTro, DangKyQuangCao, HoiNghiQT_ID")] THAMGIAHOINGHIQUOCTE ICParticipants)
        {
            if (!ModelState.IsValid)
            {
                return View(ICParticipants);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(ICParticipants).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "ICParticipants", new { id = ICParticipants.HoiNghiQT_ID });
            }
            return View(ICParticipants);
        }
    }
}