using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vinasa.DAL;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class NGUOINHANGIAITHUONGController : Controller
    {
        private SeminarContext _db = new SeminarContext();

        // GET: NguoiNhanGiaiThuong
        public ActionResult Index()
        {
            return View(_db.NGUOINHANGIAITHUONG.ToList());
        }

        // GET: NguoiNhanGiaiThuong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOINHANGIAITHUONG nguoiNhanGiaiThuong = _db.NGUOINHANGIAITHUONG.Find(id);
            if (nguoiNhanGiaiThuong == null)
            {
                return HttpNotFound();
            }
            nguoiNhanGiaiThuong.Provinces = new SelectList(_db.Provinces, "ID", "Title", nguoiNhanGiaiThuong.ProvinceId);
            nguoiNhanGiaiThuong.GiaiThuong = _db.GIAITHUONG.Find(nguoiNhanGiaiThuong.GiaiThuongId);
            return View(nguoiNhanGiaiThuong);
        }

        // GET: NguoiNhanGiaiThuong/Create
        public ActionResult Create()
        {
            var model = new NGUOINHANGIAITHUONG();
            model.Provinces = new SelectList(_db.Provinces, "ID", "Title", model.ProvinceId);
            return View(model);
        }

        // POST: NguoiNhanGiaiThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GiaiThuongId,MaSoThue,TenDonVi,TenNguoiDaiDienPhapLuat,ChucDanh,Email,DiDong,TenNguoiLienHeVoiBTC,ChucDanhNguoiLienHe,EmailNguoiLienHe,DiDongNguoiLienHe,ProvinceId,DiaChi,PhieuDangKy")] NGUOINHANGIAITHUONG nguoiNhanGiaiThuong)
        {
            if (ModelState.IsValid)
            {
                _db.NGUOINHANGIAITHUONG.Add(nguoiNhanGiaiThuong);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nguoiNhanGiaiThuong);
        }

        // GET: NguoiNhanGiaiThuong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOINHANGIAITHUONG nguoiNhanGiaiThuong = _db.NGUOINHANGIAITHUONG.Find(id);
            if (nguoiNhanGiaiThuong == null)
            {
                return HttpNotFound();
            }

            nguoiNhanGiaiThuong.Provinces = new SelectList(_db.Provinces, "ID", "Title", nguoiNhanGiaiThuong.ProvinceId);
            return View(nguoiNhanGiaiThuong);
        }

        // POST: NguoiNhanGiaiThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GiaiThuongId,MaSoThue,TenDonVi,TenNguoiDaiDienPhapLuat,ChucDanh,Email,DiDong,TenNguoiLienHeVoiBTC,ChucDanhNguoiLienHe,EmailNguoiLienHe,DiDongNguoiLienHe,ProvinceId,DiaChi,PhieuDangKy")] NGUOINHANGIAITHUONG nguoiNhanGiaiThuong)
        {
            if (!ModelState.IsValid)
            {
                nguoiNhanGiaiThuong.Provinces = new SelectList(_db.Provinces, "ID", "Title", nguoiNhanGiaiThuong.ProvinceId);
                return View(nguoiNhanGiaiThuong);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(nguoiNhanGiaiThuong).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "GIAITHUONG", new { id = nguoiNhanGiaiThuong.GiaiThuongId });
            }
            return View(nguoiNhanGiaiThuong);
        }

        // POST: NguoiNhanGiaiThuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int giaiThuongId = -1)
        {
            var nguoiNhanGiaiThuong = _db.NGUOINHANGIAITHUONG.Find(id);
            _db.NGUOINHANGIAITHUONG.Remove(nguoiNhanGiaiThuong);
            _db.SaveChanges();
            if (giaiThuongId > 0)
                return RedirectToAction("Details", "GIAITHUONG", new { id = giaiThuongId });
            else
                return RedirectToAction(nameof(Index));

        }

        public ActionResult DeleteSelected(int id, int giaiThuongId = -1)
        {
            var model = _db.NGUOINHANGIAITHUONG.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.GiaiThuongId = giaiThuongId;
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