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
    public class MemberFeePeriodController : Controller
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();
        // GET: MemberPayPeriod
        public ActionResult ManageFeesPeriod()
        {
            return View(_db.KyPhis.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KyPhi kyPhi = _db.KyPhis.Find(id);

            if (kyPhi == null)
            {
                return HttpNotFound();
            }

            return View(kyPhi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, MaSoThue, Nam, SoTienDong")] KyPhi kyPhi)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(kyPhi).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("ManageFeesPeriod");
            }

            return View(kyPhi);
        }

        public ActionResult Delete(int id)
        {
            var kyPhi = _db.KyPhis.Where(t => t.ID.Equals(id)).FirstOrDefault();

            _db.KyPhis.Remove(kyPhi);
            _db.SaveChanges();
            return RedirectToAction("ManageFeesPeriod");
        }

        public ActionResult ImportExcel(FormCollection formCollection)
        {
            int addRow = 0;
            int rowExist = 0;
            var kyPhiList = new List<KyPhi>();
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
                            string maSoThue = workSheet.Cells[rowIterator, 2].Value.ToString();
                            string tenCongTy = workSheet.Cells[rowIterator, 3].Value.ToString();

                            var kyPhi = _db.KyPhis
                                .FirstOrDefault(t => t.MaSoThue == maSoThue && t.TenCongTy == tenCongTy);
                            if (kyPhi == null)
                            {
                                var participants = new KyPhi();
                                participants.MaSoThue = workSheet.Cells[rowIterator, 2].Value.ToString();
                                participants.TenCongTy = workSheet.Cells[rowIterator, 3].Value.ToString();
                                participants.Nam = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value);
                                participants.SoTienDong = Convert.ToInt32(workSheet.Cells[rowIterator, 5].Value);
                                kyPhiList.Add(participants);
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
                    foreach (var item in kyPhiList)
                    {
                        _db.KyPhis.Add(item);
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
            return RedirectToAction("ManageFeesPeriod", "MemberFeePeriod");
        }
        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauKyPhi.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauKyPhi.xlsx");
        }
    }
}