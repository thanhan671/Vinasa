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
using Vinasa.DAL;


namespace Vinasa.Controllers
{
    public class CourseController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        // GET: Course
        public ActionResult Index()
        {
            return View(_db.KHOAHOCs.ToList());
        }
        

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MaSoThue,TenKhoaDaotao,NgayBatDau,NgayKetThuc,HinhThuc")] KHOAHOC khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _db.KHOAHOCs.Add(khoaHoc);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khoaHoc);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOAHOC khoaHoc = _db.KHOAHOCs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MaSoThue,TenKhoaDaotao,NgayBatDau,NgayKetThuc,HinhThuc")] KHOAHOC khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(khoaHoc).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khoaHoc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            KHOAHOC khoaHoc = _db.KHOAHOCs.Find(id);
            if (khoaHoc != null)
            {
                var thamGiaKhoaHocs = _db.THAMGIAKHOAHOCs.Where(it => it.IdKhoaHoc == khoaHoc.Id).ToList();
                foreach (var thamGiaKhoaHoc in thamGiaKhoaHocs)
                {
                    _db.THAMGIAKHOAHOCs.Remove(thamGiaKhoaHoc);
                }
            }

            _db.KHOAHOCs.Remove(khoaHoc);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.KHOAHOCs.Where(m => m.Id == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }
    }
}