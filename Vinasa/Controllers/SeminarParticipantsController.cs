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
    public class SeminarParticipantsController : Controller
    {
        private readonly SeminarContext _db = new SeminarContext();

        public SeminarParticipantsController()
        {
        }

        // GET: SeminarParticipants
        public ActionResult Index()
        {
            return View(_db.SeminarParticipants.ToList());
        }

        // GET: SeminarParticipants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeminarParticipant seminarParticipant = _db.SeminarParticipants.Find(id);
            if (seminarParticipant == null)
            {
                return HttpNotFound();
            }
            return View(seminarParticipant);
        }

        // GET: SeminarParticipants/Create
        public ActionResult Create()
        {
            ViewBag.Provinces = new SelectList(_db.Provinces.ToList(), "ID", "Title");
            return View();
        }

        // POST: SeminarParticipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SeminarId,Name,TaxNumber,Company,Position,Email,PhoneNumber,ProvinceId,JobTitle,Operation,RegistrySeminar,RegistryBusinessMatching,RegistryExhibition,RegistryTicket,CreatedUtc")] SeminarParticipant seminarParticipant)
        {
            if (ModelState.IsValid)
            {
                _db.SeminarParticipants.Add(seminarParticipant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seminarParticipant);
        }

        // GET: SeminarParticipants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeminarParticipant seminarParticipant = _db.SeminarParticipants.Find(id);
            if (seminarParticipant == null)
            {
                return HttpNotFound();
            }
            ViewBag.Provinces = new SelectList(_db.Provinces.ToList(), "ID", "Title");

            return View(seminarParticipant);
        }

        // POST: SeminarParticipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SeminarId,Name,TaxNumber,Company,Position,Email,PhoneNumber,ProvinceId,JobTitle,Operation,RegistrySeminar,RegistryBusinessMatching,RegistryExhibition,RegistryTicket,CreatedUtc")] SeminarParticipant seminarParticipant)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(seminarParticipant).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seminarParticipant);
        }

        // POST: SeminarParticipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int seminarId = -1)
        {
            var seminarParticipant = _db.SeminarParticipants.Find(id);
            _db.SeminarParticipants.Remove(seminarParticipant);
            _db.SaveChanges();
            if (seminarId > 0)
                return RedirectToAction("Details", "Seminars", new { id = seminarId });
            else
                return RedirectToAction(nameof(Index));

        }

        public ActionResult DeleteSelected(int id, int seminarId = -1)
        {
            var model = _db.SeminarParticipants.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.SeminarId = seminarId;
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
