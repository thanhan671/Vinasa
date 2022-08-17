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
    public class ConnectionServicesController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        // GET: ConnectionServices
        public ActionResult Index()
        {
            return View(_db.SUDUNGDICHVUKETNOIs.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUDUNGDICHVUKETNOI usingConnectionServices = _db.SUDUNGDICHVUKETNOIs.Find(id);
            if (usingConnectionServices == null)
            {
                return HttpNotFound();
            }
            return View(usingConnectionServices);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUDUNGDICHVUKETNOI usingConnectionServices = _db.SUDUNGDICHVUKETNOIs.Find(id);

            if (usingConnectionServices == null)
            {
                return HttpNotFound();
            }

            return View(usingConnectionServices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, MaSoThue, TenCongTy, NgayBatDauHopDong, NgayKetThucHopDong, TenDichVu, GiaGoc, GiaUuDai, ChiecKhauVinasa, TenNguoiLienHe, ChucDanh, Email, DienThoai, GhiChu")] SUDUNGDICHVUKETNOI usingConnectionServices)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(usingConnectionServices).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usingConnectionServices);
        }

        public ActionResult Delete(int id)
        {
            var usingConnetionServices = _db.SUDUNGDICHVUKETNOIs.Where(t => t.ID.Equals(id)).FirstOrDefault();

            _db.SUDUNGDICHVUKETNOIs.Remove(usingConnetionServices);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ImportExcel(FormCollection formCollection)
        {
            int addRow = 0;
            int rowExist = 0;
            var usingConnectionServicesList = new List<SUDUNGDICHVUKETNOI>();
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
                        if (noOfCol != 14)
                        {
                            Session["ViewBag.Success"] = null;
                            Session["ViewBag.Column"] = "Số cột dữ liệu của file không đúng mẫu, vui lòng tải mẫu Excel và thử lại !";
                        }
                        else
                        {
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                string taxNumber = workSheet.Cells[rowIterator, 2].Value.ToString();
                                string companyName = workSheet.Cells[rowIterator, 3].Value.ToString();

                                var usingConnectionServices = _db.SUDUNGDICHVUKETNOIs
                                    .FirstOrDefault(t => t.MaSoThue == taxNumber && t.TenCongTy == companyName);
                                if (usingConnectionServices == null)
                                {
                                    var participants = new SUDUNGDICHVUKETNOI();
                                    participants.MaSoThue = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    participants.TenCongTy = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    participants.NgayBatDauHopDong = Convert.ToDateTime(workSheet.Cells[rowIterator, 4].Value);
                                    participants.NgayKetThucHopDong = Convert.ToDateTime(workSheet.Cells[rowIterator, 5].Value);
                                    participants.TenDichVu = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    participants.GiaGoc = Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value);
                                    participants.GiaUuDai = Convert.ToInt32(workSheet.Cells[rowIterator, 8].Value);
                                    participants.ChiecKhauVinasa = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    participants.TenNguoiLienHe = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    participants.ChucDanh = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    participants.Email = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    participants.DienThoai = workSheet.Cells[rowIterator, 13].Value.ToString();
                                    participants.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 14].Value);
                                    usingConnectionServicesList.Add(participants);
                                    addRow++;
                                }
                                else
                                {
                                    rowExist++;
                                }
                            }
                            Session["ViewBag.Column"] = null;
                            Session["ViewBag.Success"] = addRow;
                            Session["ViewBag.Exist"] = rowExist;
                        }
                    }
                }
            }
            using (_db)
            {
                try
                {
                    foreach (var item in usingConnectionServicesList)
                    {
                        _db.SUDUNGDICHVUKETNOIs.Add(item);
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
            return RedirectToAction("Index", "ConnectionServices");
        }
        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauDichVuKetNoi.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauDichVuKetNoi.xlsx");
        }
    }
}