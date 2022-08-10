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
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
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
            THAMGIAHOINGHIQUOCTE icParticipant = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            if (icParticipant == null)
            {
                return HttpNotFound();
            }
            icParticipant.HOINGHIQUOCTE = _db.HOINGHIQUOCTEs.Find(icParticipant.HoiNghiQT_ID);
            return View(icParticipant);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAHOINGHIQUOCTE icParticipants = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            if (icParticipants == null)
            {
                return HttpNotFound();
            }

            return View(icParticipants);
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
                return RedirectToAction("Details", "IC", new { id = ICParticipants.HoiNghiQT_ID });
            }
            return View(ICParticipants);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int icId = -1)
        {
            var icParticipants = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            icId = (int)icParticipants.HoiNghiQT_ID;
            _db.THAMGIAHOINGHIQUOCTEs.Remove(icParticipants);
            _db.SaveChanges();
            if (icId > 0)
                return RedirectToAction("Details", "IC", new { id = icId });
            else
                return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteSelected(int id, int icId = -1)
        {
            var model = _db.THAMGIAHOINGHIQUOCTEs.Where(m => m.ID == id).FirstOrDefault();
            ViewBag.IdHoiNghiQT = icId;
            return PartialView("_DeleteSelected", model);
        }

    }
}