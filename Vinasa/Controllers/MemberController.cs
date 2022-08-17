using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;
using System.Net;
using Vinasa.Services;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Table;
using System.Data.Entity.Validation;
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class MemberController : Controller
    {
        #region global variable
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();
        int currentRole;
        #endregion

        #region main method
        // GET: Member
        public ActionResult ManageMember()
        {
            return View(_db.HOIVIENs.ToList());
        }

        public ActionResult DetailMember(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOIVIEN member = _db.HOIVIENs.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOIVIEN member = _db.HOIVIENs.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }

            member.RegionList = new SelectList(_db.KHUVUCs, "IDKhuVuc", "TenKhuVuc", member.KhuVuc);

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, MaSoThue, TenTiengViet, TenTiengAnh, TenVietTat, NgayThanhLap, Website, SdtCongTy, EmailCongTy, DiaChiGiaoDich, DiaChiTrenChungTu, SoLuongNhanVien," +
            "SoLuongLapTrinhVien, ThiTruongNoiDia, ThiTruongQuocTe, LinhVucHoatDong, LanhDao, ChucDanhLanhDao, SdtLanhDao, EmailLanhDao, DaiDienMarketing, ChucNangMarketing, SdtMarketing, EmailMarketing," +
            "DaiDienNhanSu, ChucDanhNhanSu, SdtNhanSu, EmailNhanSu, DaiDienKeToan, ChucDanhKeToan, SdtKeToan, EmailKeToan, Fanpage, ThoiGianGiaNhap, KhuVuc, GhiChu")] HOIVIEN member)
        {
            if (!ModelState.IsValid)
            {
                member.RegionList = new SelectList(_db.KHUVUCs, "IDKhuVuc", "TenKhuVuc", member.KhuVuc);
                return View(member);
            }
            if (ModelState.IsValid)
            {
                _db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("ManageMember");
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult ImportExcel(FormCollection formCollection)
        {
            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                return RedirectToAction("ManageMember", "Member", new { area = " " });
            }
            int addRow = 0;
            int rowExist = 0;
            var memberlist = new List<HOIVIEN>();
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
                        if (noOfCol != 36)
                        {
                            Session["ViewBag.Success"] = null;
                            Session["ViewBag.Column"] = "Số cột dữ liệu của file không đúng mẫu, vui lòng tải mẫu Excel và thử lại !";
                        }
                        else
                        {
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                string maSoThue = workSheet.Cells[rowIterator, 2].Value.ToString();
                                string tenDonVi = workSheet.Cells[rowIterator, 3].Value.ToString();
                                var hoiVien = _db.HOIVIENs
                                    .FirstOrDefault(t => t.MaSoThue == maSoThue && t.TenTiengViet == tenDonVi);
                                if (hoiVien == null)
                                {
                                    var member = new HOIVIEN();
                                    member.MaSoThue = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    member.TenTiengViet = workSheet.Cells[rowIterator, 3].Value.ToString();
                                    member.TenTiengAnh = workSheet.Cells[rowIterator, 4].Value.ToString();
                                    member.TenVietTat = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    member.NgayThanhLap = Convert.ToDateTime(workSheet.Cells[rowIterator, 6].Value);
                                    member.Website = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    member.SdtCongTy = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    member.EmailCongTy = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    member.DiaChiGiaoDich = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    member.DiaChiTrenChungTu = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    member.SoLuongNhanVien = Convert.ToInt32(workSheet.Cells[rowIterator, 12].Value);
                                    member.SoLuongLapTrinhVien = Convert.ToInt32(workSheet.Cells[rowIterator, 13].Value);
                                    member.ThiTruongNoiDia = workSheet.Cells[rowIterator, 14].Value.ToString();
                                    member.ThiTruongQuocTe = workSheet.Cells[rowIterator, 15].Value.ToString();
                                    member.LinhVucHoatDong = workSheet.Cells[rowIterator, 16].Value.ToString();
                                    member.LanhDao = workSheet.Cells[rowIterator, 17].Value.ToString();
                                    member.ChucDanhLanhDao = workSheet.Cells[rowIterator, 18].Value.ToString();
                                    member.SdtLanhDao = workSheet.Cells[rowIterator, 19].Value.ToString();
                                    member.EmailLanhDao = workSheet.Cells[rowIterator, 20].Value.ToString();
                                    member.DaiDienMarketing = workSheet.Cells[rowIterator, 21].Value.ToString();
                                    member.ChucNangMarketing = workSheet.Cells[rowIterator, 22].Value.ToString();
                                    member.SdtMarketing = workSheet.Cells[rowIterator, 23].Value.ToString();
                                    member.EmailMarketing = workSheet.Cells[rowIterator, 24].Value.ToString();
                                    member.DaiDienNhanSu = workSheet.Cells[rowIterator, 25].Value.ToString();
                                    member.ChucDanhNhanSu = workSheet.Cells[rowIterator, 26].Value.ToString();
                                    member.SdtNhanSu = workSheet.Cells[rowIterator, 27].Value.ToString();
                                    member.EmailNhanSu = workSheet.Cells[rowIterator, 28].Value.ToString();
                                    member.DaiDienKeToan = workSheet.Cells[rowIterator, 29].Value.ToString();
                                    member.ChucDanhKeToan = workSheet.Cells[rowIterator, 30].Value.ToString();
                                    member.SdtKeToan = workSheet.Cells[rowIterator, 31].Value.ToString();
                                    member.EmailKeToan = workSheet.Cells[rowIterator, 32].Value.ToString();
                                    member.Fanpage = workSheet.Cells[rowIterator, 33].Value.ToString();
                                    member.ThoiGianGiaNhap = Convert.ToDateTime(workSheet.Cells[rowIterator, 34].Value);
                                    member.KhuVuc = Convert.ToInt32(workSheet.Cells[rowIterator, 35].Value);
                                    member.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 36].Value);
                                    memberlist.Add(member);
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
                    foreach (var item in memberlist)
                    {
                        _db.HOIVIENs.Add(item);
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
            return RedirectToAction("ManageMember", "Member", new { area = " " });
        }



        public ActionResult Delete(int id)
        {
            var memberAccount = _db.HOIVIENs.Where(t => t.ID.Equals(id)).FirstOrDefault();

            if (memberAccount != null)
            {
                _db.HOIVIENs.Remove(memberAccount);
                _db.SaveChanges();
            }
            return RedirectToAction("ManageMember", "Member", new { area = " " });
        }

        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauHoiVien.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauHoiVien.xlsx");
        }
        #endregion

        #region support method
        private void CheckRole()
        {
            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                TempData["Message"] = "Không thể thực hiện hành động này với tài khoản quản lí";
            }
        }
        #endregion
    }
}