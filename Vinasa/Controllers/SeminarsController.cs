﻿using System;
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
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class SeminarsController : Controller
    {
        private readonly SeminarContext _db = new SeminarContext();
        private readonly ImportManager _importManager;
        public SeminarsController()
        {
            _importManager = new ImportManager(_db);

        }

        // GET: Seminars
        public ActionResult Index()
        {
            return View(_db.Seminars.OrderByDescending(it => it.Id).ToList());
        }

        // GET: Seminars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = _db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }

            seminar.SeminarParticipants = _db.SeminarParticipants.Where(m => m.SeminarId == seminar.Id).OrderByDescending(it => it.Id).ToList();

            return View(seminar);
        }

        // GET: Seminars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seminars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,OpenDate,CloseDate,Address,CreatedUtc")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                _db.Seminars.Add(seminar);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seminar);
        }

        // GET: Seminars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = _db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // POST: Seminars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,OpenDate,CloseDate,Address,CreatedUtc")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(seminar).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seminar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Seminar seminar = _db.Seminars.Find(id);
            if (seminar != null)
            {
                var seminarParticipants = _db.SeminarParticipants.Where(it => it.SeminarId == seminar.Id).ToList();
                foreach (var seminarParticipant in seminarParticipants)
                {
                    _db.SeminarParticipants.Remove(seminarParticipant);
                }
            }

            _db.Seminars.Remove(seminar);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.Seminars.Where(m => m.Id == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }

        [HttpPost, ActionName("ImportExcel")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ImportExcel(int? id, HttpPostedFileBase importExcelFile)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (importExcelFile != null && importExcelFile.ContentLength > 0)
                {
                    var tuple = await _importManager.ImportSeminarParticipantFromXlsx((int)id, importExcelFile);
                    Session["ViewBag.Success"] = tuple.Item1;
                    Session["ViewBag.Exist"] = tuple.Item2;
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception exc)
            {
                Session["ViewBag.Error"] = exc.Message;
                return RedirectToAction(nameof(Details), new { id = id, erorr = exc });
            }
        }
        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauHoiNghi.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauHoiNghi.xlsx");
        }
    }
}