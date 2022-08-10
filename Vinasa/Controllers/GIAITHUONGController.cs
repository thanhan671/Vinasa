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
using Vinasa.DAL;
using Vinasa.Models;
using Vinasa.Services;
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class GIAITHUONGController : Controller
    {
        private SeminarContext _db = new SeminarContext();
        private readonly ImportManager _importManager;
        public GIAITHUONGController()
        {
            _importManager = new ImportManager(_db);

        }
        // GET: GiaiThuong
        public ActionResult Index()
        {
            return View(_db.GIAITHUONG.ToList());
        }

        // GET: GiaiThuong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAITHUONG giaiThuong = _db.GIAITHUONG.Find(id);
            if (giaiThuong == null)
            {
                return HttpNotFound();
            }

            giaiThuong.NguoiNhanGiaiThuong = _db.NGUOINHANGIAITHUONG.Where(m => m.GiaiThuongId == giaiThuong.Id).ToList();

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
        public ActionResult Create([Bind(Include = "Id,Title,OpenDate,Address,CreatedUtc")] GIAITHUONG giaiThuong)
        {
            if (ModelState.IsValid)
            {
                _db.GIAITHUONG.Add(giaiThuong);
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
            GIAITHUONG giaiThuong = _db.GIAITHUONG.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Title,OpenDate,Address,CreatedUtc")] GIAITHUONG giaiThuong)
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
            GIAITHUONG giaiThuong = _db.GIAITHUONG.Find(id);
            if (giaiThuong != null)
            {
                var giaiThuongParticipants = _db.NGUOINHANGIAITHUONG.Where(it => it.GiaiThuongId == giaiThuong.Id).ToList();
                foreach (var giaiThuongParticipant in giaiThuongParticipants)
                {
                    _db.NGUOINHANGIAITHUONG.Remove(giaiThuongParticipant);
                }
            }

            _db.GIAITHUONG.Remove(giaiThuong);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.GIAITHUONG.Where(m => m.Id == id).FirstOrDefault();
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

                    var tuple = await _importManager.ImportNguoiNhanGiaiThuongsFromXlsx((int)id, importexcelfile.InputStream);
                    Session["ViewBag.Success"] = tuple.Item1;
                    Session["ViewBag.Exist"] = tuple.Item2;
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
        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauGiaiThuong.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauGiaiThuong.xlsx");
        }
    }
}