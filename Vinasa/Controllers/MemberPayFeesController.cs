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
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class MemberPayFeesController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();
        // GET: MemberFees
        public ActionResult ManagePayFees()
        {
            return View(_db.DongPhis.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongPhi dongPhi = _db.DongPhis.Find(id);

            if (dongPhi == null)
            {
                return HttpNotFound();
            }

            return View(dongPhi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, MaSoThue, TenCongTy, NguoiNhanPhieuThu, Sdt, NgayChuyenTien, NgayGuiPhieuThu, SoTienDong, GhiChu")] DongPhi dongPhi)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(dongPhi).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("ManagePayFees");
            }

            return View(dongPhi);
        }

        public ActionResult Delete(int id)
        {
            var dongPhi = _db.DongPhis.Where(t => t.ID.Equals(id)).FirstOrDefault();

            _db.DongPhis.Remove(dongPhi);
            _db.SaveChanges();
            return RedirectToAction("ManagePayFees");
        }

        public ActionResult ImportExcel(FormCollection formCollection)
        {
            int addRow = 0;
            int rowExist = 0;
            var dongPhiList = new List<DongPhi>();
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
                            string taxNumber = workSheet.Cells[rowIterator, 2].Value.ToString();
                            string companyName = workSheet.Cells[rowIterator, 3].Value.ToString();

                            var dongPhi = _db.DongPhis
                                .FirstOrDefault(t => t.MaSoThue == taxNumber && t.TenCongTy == companyName);
                            if (dongPhi == null)
                            {
                                var participants = new DongPhi();
                                participants.MaSoThue = workSheet.Cells[rowIterator, 2].Value.ToString();
                                participants.TenCongTy = workSheet.Cells[rowIterator, 3].Value.ToString();
                                participants.NguoiNhanPhieuThu = workSheet.Cells[rowIterator, 4].Value.ToString();
                                participants.Sdt = workSheet.Cells[rowIterator, 5].Value.ToString();
                                participants.NgayChuyenTien = Convert.ToDateTime(workSheet.Cells[rowIterator, 6].Value);
                                participants.NgayGuiPhieuThu = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value);
                                participants.SoTienDong = Convert.ToInt32(workSheet.Cells[rowIterator, 8].Value);
                                participants.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 9].Value);
                                dongPhiList.Add(participants);
                                addRow++;
                            }
                            else
                            {
                                rowExist++;
                            }
                        }
                    }
                }
            }
            using (_db)
            {
                try
                {
                    foreach (var item in dongPhiList)
                    {
                        _db.DongPhis.Add(item);
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
            Session["ViewBag.Success"] = addRow;
            Session["ViewBag.Exist"] = rowExist;
            return RedirectToAction("ManagePayFees", "MemberFees");
        }
        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauDongPhi.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauDongPhi.xlsx");
        }
    }
}