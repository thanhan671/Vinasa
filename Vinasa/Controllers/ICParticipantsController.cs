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
using OfficeOpenXml;
using System.Data.Entity.Validation;

namespace Vinasa.Controllers
{
    public class ICParticipantsController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        public ActionResult Index()
        {
            return View(_db.THAMGIAHOINGHIQUOCTEs.ToList());
        }

        // GET: NguoiNhanGiaiThuong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAHOINGHIQUOCTE ICParticipants = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            if (ICParticipants == null)
            {
                return HttpNotFound();
            }
            ICParticipants.HOINGHIQUOCTE = _db.HOINGHIQUOCTEs.Find(ICParticipants.ID);
            return View(ICParticipants);
        }

        // GET: NguoiNhanGiaiThuong/Create
        public ActionResult Create()
        {
            var model = new THAMGIAHOINGHIQUOCTE();
            //model.Provinces = new SelectList(_db.Provinces, "ID", "Title", model.ProvinceId);
            return View(model);
        }

        // POST: NguoiNhanGiaiThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, TenDonVi, DonViChungToiLa, DiaChi, DienThoai, DaiDienLienHe, ChucVu, DiDong, " +
            "Email, DangKyThamDu, DangKyPhatBieu, DangKyGianHangTrienLam, DangKyBusinessMatchingOnline, DangKyBusinessMatchingOffline, DangKyTaiTro, DangKyQuangCao, HoiNghiQT_ID")] THAMGIAHOINGHIQUOCTE ICParticipants)
        {
            if (ModelState.IsValid)
            {
                _db.THAMGIAHOINGHIQUOCTEs.Add(ICParticipants);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ICParticipants);
        }

        // GET: NguoiNhanGiaiThuong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIAHOINGHIQUOCTE ICParticipants = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            if (ICParticipants == null)
            {
                return HttpNotFound();
            }

            return View(ICParticipants);
        }

        // POST: NguoiNhanGiaiThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, TenDonVi, DonViChungToiLa, DiaChi, DienThoai, DaiDienLienHe, ChucVu, DiDong, Email, DangKyThamDu, DangKyPhatBieu, DangKyGianHangTrienLam, DangKyBusinessMatchingOnline, DangKyBusinessMatchingOffline, DangKyTaiTro, DangKyQuangCao, HoiNghiQT_ID")] THAMGIAHOINGHIQUOCTE ICParticipants)
        {
            if (!ModelState.IsValid)
            {
                return View(ICParticipants);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(ICParticipants).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", "IC", new { id = ICParticipants.ID });
            }
            return View(ICParticipants);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int hoiNghiQTId = -1)
        {
            var thamGiaHoiThaoQT = _db.THAMGIAHOINGHIQUOCTEs.Find(id);
            _db.THAMGIAHOINGHIQUOCTEs.Remove(thamGiaHoiThaoQT);
            _db.SaveChanges();
            if (hoiNghiQTId > 0)
                return RedirectToAction("Details", "IC", new { id = hoiNghiQTId });
            else
                return RedirectToAction(nameof(Index));

        }

        public ActionResult DeleteSelected(int id, int hoiNghiQTId = -1)
        {
            var model = _db.THAMGIAHOINGHIQUOCTEs.Where(m => m.ID == id).FirstOrDefault();
            ViewBag.HoiNghiQT_ID = hoiNghiQTId;
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
        public ActionResult ImportExcel(FormCollection formCollection)
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
                            icParticipantsList.Add(participants);
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

            return RedirectToAction("Details", "ICParticipants", new { area = " " });
        }
    }
}