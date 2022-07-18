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
                return RedirectToAction("Index", "CourseParticipants", new { id = courseParticipants.IdKhoaHoc });
            }
            return View(courseParticipants);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int courseId = -1)
        {
            var courseParticipants = _db.THAMGIAKHOAHOCs.Find(id);
            _db.THAMGIAKHOAHOCs.Remove(courseParticipants);
            _db.SaveChanges();
            if (courseId > 0)
                return RedirectToAction("Index", "CourseParticipants", new { id = courseId });
            else
                return RedirectToAction(nameof(Index));

        }

        public ActionResult DeleteSelected(int id, int courseId = -1)
        {
            var model = _db.THAMGIAKHOAHOCs.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.IdKhoahoc = courseId;
            return PartialView("_DeleteSelected", model);
        }

        public ActionResult ImportExcel(FormCollection formCollection)
        {
            var courseParticipantsList = new List<THAMGIAKHOAHOC>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var participants = new THAMGIAKHOAHOC();
                            participants.HoTen = workSheet.Cells[rowIterator, 2].Value.ToString();
                            participants.CongTyToChucCoQuan = workSheet.Cells[rowIterator, 3].Value.ToString();
                            participants.ChucDanh = workSheet.Cells[rowIterator, 4].Value.ToString();
                            participants.Email = workSheet.Cells[rowIterator, 5].Value.ToString();
                            participants.Sdt = workSheet.Cells[rowIterator, 6].Value.ToString();
                            participants.SoLuongHocVien = Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value);
                            participants.HoiVienVinasa = Convert.ToBoolean(workSheet.Cells[rowIterator, 8].Value);
                            courseParticipantsList.Add(participants);
                        }
                    }
                }
            }
            using (_db)
            {
                try
                {
                    foreach (var item in courseParticipantsList)
                    {
                        _db.THAMGIAKHOAHOCs.Add(item);
                    }
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        throw new HttpException(eve.Entry.Entity.GetType().Name);
                    }
                    throw new HttpException(e.ToString());
                }
            }

            return RedirectToAction("Index", "CourseParticipants", new { area = " " });
        }

    }
}