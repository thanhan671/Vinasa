using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vinasa.DAL;
using Vinasa.Models;
using Vinasa.Services;

namespace Vinasa.Controllers
{
    public class GiaiThuongsController : Controller
    {
        private SeminarContext _db = new SeminarContext();
        private readonly ImportManager _importManager;
        public GiaiThuongsController()
        {
            _importManager = new ImportManager(_db);

        }
        // GET: GiaiThuong
        public ActionResult Index()
        {
            return View(_db.GiaiThuong.ToList());
        }

        // GET: GiaiThuong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaiThuong giaiThuong = _db.GiaiThuong.Find(id);
            if (giaiThuong == null)
            {
                return HttpNotFound();
            }

            giaiThuong.NguoiNhanGiaiThuong = _db.NguoiNhanGiaiThuongs.Where(m => m.GiaiThuongId == giaiThuong.Id).ToList();

            return View(giaiThuong);
        }

        // GET: GiaiThuong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiaiThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,OpenDate,Address,CreatedUtc")] GiaiThuong giaiThuong)
        {
            if (ModelState.IsValid)
            {
                _db.GiaiThuong.Add(giaiThuong);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giaiThuong);
        }

        // GET: GiaiThuong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiaiThuong giaiThuong = _db.GiaiThuong.Find(id);
            if (giaiThuong == null)
            {
                return HttpNotFound();
            }
            return View(giaiThuong);
        }

        // POST: GiaiThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,OpenDate,Address,CreatedUtc")] GiaiThuong giaiThuong)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(giaiThuong).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giaiThuong);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            GiaiThuong giaiThuong = _db.GiaiThuong.Find(id);
            if (giaiThuong != null)
            {
                var giaiThuongParticipants = _db.NguoiNhanGiaiThuongs.Where(it => it.GiaiThuongId == giaiThuong.Id).ToList();
                foreach (var giaiThuongParticipant in giaiThuongParticipants)
                {
                    _db.NguoiNhanGiaiThuongs.Remove(giaiThuongParticipant);
                }
            }

            _db.GiaiThuong.Remove(giaiThuong);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.GiaiThuong.Where(m => m.Id == id).FirstOrDefault();
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
                    _db.NguoiNhanGiaiThuongs.RemoveRange(_db.NguoiNhanGiaiThuongs.ToList());

                    await _importManager.ImportNguoiNhanGiaiThuongsFromXlsx((int)id, importexcelfile.InputStream);
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
