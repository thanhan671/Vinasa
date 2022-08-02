using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;
using OfficeOpenXml;
using System.Data.Entity.Validation;

namespace Vinasa.Controllers
{
    public class ICController : Controller
    {

        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        // GET: Course
        public ActionResult Index()
        {
            return View(_db.HOINGHIQUOCTEs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOINGHIQUOCTE ic = _db.HOINGHIQUOCTEs.Find(id);
            if (ic == null)
            {
                return HttpNotFound();
            }

            ic.THAMGIAHOINGHIQUOCTEs = _db.THAMGIAHOINGHIQUOCTEs.Where(m => m.HoiNghiQT_ID == ic.ID).ToList();

            return View(ic);
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
            HOINGHIQUOCTE hoiNghi = _db.HOINGHIQUOCTEs.Find(id);
            if (hoiNghi == null)
            {
                return HttpNotFound();
            }

            //khoaHoc.isEdit = true;

            return View(hoiNghi);
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
            HOINGHIQUOCTE hoiNghi = _db.HOINGHIQUOCTEs.Find(id);
            if (hoiNghi != null)
            {
                var thamGiaHoiNGhis = _db.THAMGIAHOINGHIQUOCTEs.Where(it => it.HoiNghiQT_ID == hoiNghi.ID).ToList();
                foreach (var thamGiaHoiNghi in thamGiaHoiNGhis)
                {
                    _db.THAMGIAHOINGHIQUOCTEs.Remove(thamGiaHoiNghi);
                }
            }

            _db.HOINGHIQUOCTEs.Remove(hoiNghi);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.HOINGHIQUOCTEs.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }

        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauHoiNghiQuocTe.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauHoiNghiQuocTe.xlsx");
        }

        public ActionResult ImportExcel(int? id, FormCollection formCollection)
        {
            var icParticipantsList = new List<THAMGIAHOINGHIQUOCTE>();
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
                            string tenDonVi = workSheet.Cells[rowIterator, 1].Value.ToString();
                            var THAMGIAHOINGHI = _db.THAMGIAHOINGHIQUOCTEs
                                .FirstOrDefault(t => t.TenDonVi == tenDonVi && t.HoiNghiQT_ID == id);
                            if (THAMGIAHOINGHI == null)
                            {
                                var participants = new THAMGIAHOINGHIQUOCTE();
                                participants.TenDonVi = workSheet.Cells[rowIterator, 1].Value.ToString();
                                participants.DonViChungToiLa = workSheet.Cells[rowIterator, 2].Value.ToString();
                                participants.DiaChi = workSheet.Cells[rowIterator, 3].Value.ToString();
                                participants.DienThoai = workSheet.Cells[rowIterator, 4].Value.ToString();
                                participants.DaiDienLienHe = workSheet.Cells[rowIterator, 5].Value.ToString();
                                participants.ChucVu = workSheet.Cells[rowIterator, 6].Value.ToString();
                                participants.DiDong = workSheet.Cells[rowIterator, 7].Value.ToString();
                                participants.Email = workSheet.Cells[rowIterator, 8].Value.ToString();
                                participants.DangKyThamDu = Convert.ToBoolean(workSheet.Cells[rowIterator, 9].Value);
                                participants.DangKyPhatBieu = Convert.ToBoolean(workSheet.Cells[rowIterator, 10].Value);
                                participants.DangKyGianHangTrienLam = Convert.ToBoolean(workSheet.Cells[rowIterator, 11].Value);
                                participants.DangKyBusinessMatchingOnline = Convert.ToBoolean(workSheet.Cells[rowIterator, 12].Value);
                                participants.DangKyBusinessMatchingOffline = Convert.ToBoolean(workSheet.Cells[rowIterator, 13].Value);
                                participants.DangKyTaiTro = Convert.ToBoolean(workSheet.Cells[rowIterator, 14].Value);
                                participants.DangKyQuangCao = Convert.ToBoolean(workSheet.Cells[rowIterator, 15].Value);
                                participants.HoiNghiQT_ID = id;
                                icParticipantsList.Add(participants);
                            }

                        }
                    }
                }
            }
            using (_db)
            {
                try
                {
                    foreach (var item in icParticipantsList)
                    {
                        _db.THAMGIAHOINGHIQUOCTEs.Add(item);
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
            return RedirectToAction("Details", "IC", new { id });
        }

    }
}