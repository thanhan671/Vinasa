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
    public class HoiPhiController : Controller
    {
        private SeminarContext db = new SeminarContext();

        // GET: HoiPhi
        public ActionResult Index()
        {
            return View(db.HoiPhi.ToList());
        }

        // GET: HoiPhi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiPhi hoiPhi = db.HoiPhi.Find(id);
            if (hoiPhi == null)
            {
                return HttpNotFound();
            }
            return View(hoiPhi);
        }

        // GET: HoiPhi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoiPhi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MaSoThue,TenCongTy,DiaChiGhiPhieuThu,DiaChiGuiPhieuThu,NguoiNhanPhieu,DienThoai,HoiPhiNamTruoc,HoiPhiNamSau,TongThu,DaDong,ConLai,NgayChuyenTien,NgayGuiPhieuThu,GhiChu")] HoiPhi hoiPhi)
        {
            if (ModelState.IsValid)
            {
                db.HoiPhi.Add(hoiPhi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hoiPhi);
        }

        // GET: HoiPhi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiPhi hoiPhi = db.HoiPhi.Find(id);
            if (hoiPhi == null)
            {
                return HttpNotFound();
            }
            return View(hoiPhi);
        }

        // POST: HoiPhi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MaSoThue,TenCongTy,DiaChiGhiPhieuThu,DiaChiGuiPhieuThu,NguoiNhanPhieu,DienThoai,HoiPhiNamTruoc,HoiPhiNamSau,TongThu,DaDong,ConLai,NgayChuyenTien,NgayGuiPhieuThu,GhiChu")] HoiPhi hoiPhi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoiPhi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MemberFee", "Statistic");
            }
            return View(hoiPhi);
        }

        // GET: HoiPhi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoiPhi hoiPhi = db.HoiPhi.Find(id);
            if (hoiPhi == null)
            {
                return HttpNotFound();
            }
            return View(hoiPhi);
        }

        // POST: HoiPhi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoiPhi hoiPhi = db.HoiPhi.Find(id);
            db.HoiPhi.Remove(hoiPhi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
