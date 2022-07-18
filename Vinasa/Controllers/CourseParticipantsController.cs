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
                return RedirectToAction("Index", "CourseParticipants", new { id = courseParticipants.IdKhoaHoc });
            }
            return View(courseParticipants);
        }
    }
}