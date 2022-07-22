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
    public class CourseParticipantsController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();


        public ActionResult Index()
        {
            return View(_db.THAMGIAKHOAHOCs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAKHOAHOC courseParticipant = _db.THAMGIAKHOAHOCs.Find(id);
            if (courseParticipant == null)
            {
                return HttpNotFound();
            }
            courseParticipant.KHOAHOC = _db.KHOAHOCs.Find(courseParticipant.IdKhoaHoc);
            return View(courseParticipant);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAKHOAHOC courseParticipants = _db.THAMGIAKHOAHOCs.Find(id);
            if (courseParticipants == null)
            {
                return HttpNotFound();
            }

            return View(courseParticipants);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, HoTen, CongTyToChucCoQuan, ChucDanh, Email, Sdt, SoLuongHocVien, HoiVienVinasa, IdKhoaHoc")] THAMGIAKHOAHOC courseParticipants)
        {
            if (!ModelState.IsValid)
            {
                return View(courseParticipants);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(courseParticipants).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "Course", new { id = courseParticipants.IdKhoaHoc });
            }
            return View(courseParticipants);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int courseId = -1)
        {
            var courseParticipants = _db.THAMGIAKHOAHOCs.Find(id);
            courseId = (int)courseParticipants.IdKhoaHoc;
            _db.THAMGIAKHOAHOCs.Remove(courseParticipants);
            _db.SaveChanges();
            if (courseId > 0)
                return RedirectToAction("Details", "Course", new { id = courseId });
            else
                return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteSelected(int id, int courseId = -1)
        {
            var model = _db.THAMGIAKHOAHOCs.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.IdKhoahoc = courseId;
            return PartialView("_DeleteSelected", model);
        }

    }
}