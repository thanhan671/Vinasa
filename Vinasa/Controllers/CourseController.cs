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
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class CourseController : Controller
    {

        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        // GET: Course
        public ActionResult Index()
        {
            return View(_db.KHOAHOCs.OrderByDescending(it => it.Id).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOAHOC course = _db.KHOAHOCs.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            course.THAMGIAKHOAHOCs = _db.THAMGIAKHOAHOCs.Where(m => m.IdKhoaHoc == course.Id).OrderByDescending(it => it.Id).ToList();

            return View(course);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenKhoaDaoTao,NgayBatDau,NgayKetThuc,HinhThuc,TenGiangVien,DiaDiem,HocPhi")] KHOAHOC khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _db.KHOAHOCs.Add(khoaHoc);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khoaHoc);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOAHOC khoaHoc = _db.KHOAHOCs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }

            //khoaHoc.isEdit = true;

            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenKhoaDaotao,NgayBatDau,NgayKetThuc,HinhThuc,TenGiangVien,DiaDiem,HocPhi")] KHOAHOC khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(khoaHoc).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                //khoaHoc.isEdit = false;
                return RedirectToAction("Index");
            }

            return View(khoaHoc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            KHOAHOC khoaHoc = _db.KHOAHOCs.Find(id);
            if (khoaHoc != null)
            {
                var thamGiaKhoaHocs = _db.THAMGIAKHOAHOCs.Where(it => it.IdKhoaHoc == khoaHoc.Id).ToList();
                foreach (var thamGiaKhoaHoc in thamGiaKhoaHocs)
                {
                    _db.THAMGIAKHOAHOCs.Remove(thamGiaKhoaHoc);
                }
            }

            _db.KHOAHOCs.Remove(khoaHoc);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.KHOAHOCs.Where(m => m.Id == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }

        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauKhoaDaoTao.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauKhoaDaoTao.xlsx");
        }

        public ActionResult ImportExcel(int? id, FormCollection formCollection)
        {
            int addRow = 0;
            int rowExist = 0;
            var courseParticipantsList = new List<THAMGIAKHOAHOC>();
            HttpPostedFileBase file = Request.Files["UploadedFile"];
            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName).ToLower();
            if (extension != ".xls" && extension != ".xlsx")
            {
                Session["ViewBag.Success"] = null;
                Session["ViewBag.Column"] = null;
                Session["ViewBag.Size"] = null;
                Session["ViewBag.File"] = "Chỉ hỗ trợ các tệp có đuôi .xls; .xlsx !";
            }
            else
            {
                if (file.ContentLength > 1024 * 1024 * 100)
                {
                    Session["ViewBag.Success"] = null;
                    Session["ViewBag.Column"] = null;
                    Session["ViewBag.Size"] = "Dung lượng file tải lên không vượt quá 100MB";
                }
                else
                {
                    if (Request != null)
                    {
                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                if (noOfCol != 7)
                                {
                                    Session["ViewBag.Success"] = null;
                                    Session["ViewBag.Column"] = "Số cột dữ liệu của file không đúng mẫu, vui lòng tải mẫu Excel và thử lại !";
                                }
                                else
                                {
                                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                    {
                                        string hoTen = workSheet.Cells[rowIterator, 1].Value.ToString();
                                        var THAMGIAKHOAHOC = _db.THAMGIAKHOAHOCs
                                            .FirstOrDefault(t => t.HoTen == hoTen && t.IdKhoaHoc == id);
                                        if (THAMGIAKHOAHOC == null)
                                        {
                                            var participants = new THAMGIAKHOAHOC();
                                            participants.HoTen = workSheet.Cells[rowIterator, 1].Value.ToString();
                                            participants.CongTyToChucCoQuan = workSheet.Cells[rowIterator, 2].Value.ToString();
                                            participants.ChucDanh = workSheet.Cells[rowIterator, 3].Value.ToString();
                                            participants.Email = workSheet.Cells[rowIterator, 4].Value.ToString();
                                            participants.Sdt = workSheet.Cells[rowIterator, 5].Value.ToString();
                                            participants.SoLuongHocVien = Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value);
                                            participants.HoiVienVinasa = Convert.ToBoolean(workSheet.Cells[rowIterator, 7].Value);
                                            participants.IdKhoaHoc = id;
                                            courseParticipantsList.Add(participants);
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
                }
            }
            return RedirectToAction("Details", "Course", new { id });
        }

    }
}