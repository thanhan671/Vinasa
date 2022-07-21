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
            return View(_db.Seminars.ToList());
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

            seminar.SeminarParticipants = _db.SeminarParticipants.Where(m => m.SeminarId == seminar.Id).ToList();

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
            if(seminar != null)
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
        public async Task<ActionResult> ImportExcel(int? id, HttpPostedFileBase importexcelfile)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (importexcelfile != null && importexcelfile.ContentLength > 0)
                {
                    _db.SeminarParticipants.RemoveRange(_db.SeminarParticipants.ToList());

                    await _importManager.ImportSeminarParticipantFromXlsx((int)id, importexcelfile.InputStream);
                }
                else
                {
                    return RedirectToAction(nameof(Details), new { id = id, erorr = "error" });
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception exc)
            {
                return RedirectToAction(nameof(Details), new { id = id, erorr = exc });
            }
        }
    }
}
