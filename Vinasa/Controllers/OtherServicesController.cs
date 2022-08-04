using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class OtherServicesController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();
        // GET: OtherServices
        public ActionResult Index()
        {
            return View(_db.SUDUNGDICHVUKHACs.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUDUNGDICHVUKHAC otherParticipant = _db.SUDUNGDICHVUKHACs.Find(id);
            if (otherParticipant == null)
            {
                return HttpNotFound();
            }
            return View(otherParticipant);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUDUNGDICHVUKHAC otherParticipants = _db.SUDUNGDICHVUKHACs.Find(id);
            if (otherParticipants == null)
            {
                return HttpNotFound();
            }

            return View(otherParticipants);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaSoThue,TenCongTy,NgayBatDauHopDong,NgayKetThucHopDong,TenDichVuKhac,GiaGoc," +
            "GiaUuDai,ChiecKhauVinasa,TenNguoiLienHe,ChucDanh,Email,DienThoai,GhiChu")] SUDUNGDICHVUKHAC otherParticipants)
        {
            if (!ModelState.IsValid)
            {
                return View(otherParticipants);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(otherParticipants).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "OtherServices");
            }
            return View(otherParticipants);
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