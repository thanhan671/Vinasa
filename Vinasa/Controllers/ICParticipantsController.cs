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

        // GET: NguoiNhanGiaiThuong/Details/5
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
            ICParticipants.HOINGHIQUOCTE = _db.HOINGHIQUOCTEs.Find(ICParticipants.ID);
            return View(ICParticipants);
        }

        // GET: NguoiNhanGiaiThuong/Create
        public ActionResult Create()
        {
            var model = new THAMGIAHOINGHIQUOCTE();
            //model.Provinces = new SelectList(_db.Provinces, "ID", "Title", model.ProvinceId);
            return View(model);
        }

        // POST: NguoiNhanGiaiThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, TenDonVi, DonViChungToiLa, DiaChi, DienThoai, DaiDienLienHe, ChucVu, DiDong, " +
            "Email, DangKyThamDu, DangKyPhatBieu, DangKyGianHangTrienLam, DangKyBusinessMatchingOnline, DangKyBusinessMatchingOffline, DangKyTaiTro, DangKyQuangCao, HoiNghiQT_ID")] THAMGIAHOINGHIQUOCTE ICParticipants)
        {
            if (ModelState.IsValid)
            {
                _db.THAMGIAHOINGHIQUOCTEs.Add(ICParticipants);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ICParticipants);
        }

        // GET: NguoiNhanGiaiThuong/Edit/5
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

        // POST: NguoiNhanGiaiThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, TenDonVi, DonViChungToiLa, DiaChi, DienThoai, DaiDienLienHe, ChucVu, DiDong, Email, DangKyThamDu, DangKyPhatBieu, DangKyGianHangTrienLam, DangKyBusinessMatchingOnline, DangKyBusinessMatchingOffline, DangKyTaiTro, DangKyQuangCao, HoiNghiQT_ID")] THAMGIAHOINGHIQUOCTE ICParticipants)
        {
            if (!ModelState.IsValid)
            {
                return View(ICParticipants);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(ICParticipants).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "IC", new { id = ICParticipants.ID });
            }
            return View(ICParticipants);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int hoiNghiQTId = -1)
        {
            var thamGiaHoiThaoQT = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            _db.THAMGIAHOINGHIQUOCTEs.Remove(thamGiaHoiThaoQT);
            _db.SaveChanges();
            if (hoiNghiQTId > 0)
                return RedirectToAction("Details", "IC", new { id = hoiNghiQTId });
            else
                return RedirectToAction(nameof(Index));

        }

        public ActionResult DeleteSelected(int id, int hoiNghiQTId = -1)
        {
            var model = _db.THAMGIAHOINGHIQUOCTEs.Where(m => m.ID == id).FirstOrDefault();
            ViewBag.HoiNghiQT_ID = hoiNghiQTId;
            return PartialView("_DeleteSelected", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}