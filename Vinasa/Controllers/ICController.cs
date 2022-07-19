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
    public class ICController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();
        // GET: IC
        public ActionResult Index()
        {
            return View(_db.HOINGHIQUOCTEs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ten,DiaDiem,ThoiGianBatDau,ThoiGianKetThuc")] HOINGHIQUOCTE hoinghiquocte)
        {
            if (ModelState.IsValid)
            {
                _db.HOINGHIQUOCTEs.Add(hoinghiquocte);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hoinghiquocte);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOINGHIQUOCTE hoinghiquocte = _db.HOINGHIQUOCTEs.Find(id);
            if (hoinghiquocte == null)
            {
                return HttpNotFound();
            }
            return View(hoinghiquocte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ten,DiaDiem,ThoiGianBatDau,ThoiGianKetThuc")] HOINGHIQUOCTE hoinghiquocte)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(hoinghiquocte).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hoinghiquocte);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            HOINGHIQUOCTE hoinghiquocte = _db.HOINGHIQUOCTEs.Find(id);
            if (hoinghiquocte != null)
            {
                var thamGiaHoiNghis = _db.THAMGIAHOINGHIQUOCTEs.Where(it => it.HoiNghiQT_ID == hoinghiquocte.ID).ToList();
                foreach (var thamGiaHoiNghi in thamGiaHoiNghis)
                {
                    _db.THAMGIAHOINGHIQUOCTEs.Remove(thamGiaHoiNghi);
                }
            }

            _db.HOINGHIQUOCTEs.Remove(hoinghiquocte);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.HOINGHIQUOCTEs.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }
    }
}