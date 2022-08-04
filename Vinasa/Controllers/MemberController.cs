using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;
using Vinasa.Services;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Table;
using System.Data.Entity.Validation;

namespace Vinasa.Controllers
{
    public class MemberController : Controller
    {
        #region global variable
        SEP25Team16Entities2 db = new SEP25Team16Entities2();
        int currentRole;
        #endregion

        #region main method
        // GET: Member
        [HttpGet]
        public ActionResult ManageMember()
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }
            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                TempData["Message"] = "Lưu ý không thể xóa với tài khoản quản lí";
            }

            var data = db.HOIVIENs.ToList();
            ViewBag.hoivienDetail = data;
            return View();
        }

        [HttpPost]
        public ActionResult ManageMember(int memberID)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            CheckRole();

            MemberAccountModels memberAccountModels = db.HOIVIENs.Where(mAcc => mAcc.ID.Equals(id)).Select(mAcc => new MemberAccountModels()
            {
                ID = mAcc.ID,
                MaSoThue = mAcc.MaSoThue,
                TenTiengViet = mAcc.TenTiengViet,
                TenTiengAnh = mAcc.TenTiengAnh,
                TenVietTat = mAcc.TenVietTat,
                NgayThanhLap = mAcc.NgayThanhLap,
                Website = mAcc.Website,
                SdtCongTy = mAcc.SdtCongTy,
                EmailCongTy = mAcc.EmailCongTy,
                DiaChiGiaoDich = mAcc.DiaChiGiaoDich,
                DiaChiTrenChungTu = mAcc.DiaChiTrenChungTu,
                SoLuongNhanVien = mAcc.SoLuongNhanVien,
                SoLuongLapTrinhVien = mAcc.SoLuongLapTrinhVien,
                ThiTruongNoiDia = mAcc.ThiTruongNoiDia,
                ThiTruongQuocTe = mAcc.ThiTruongQuocTe,
                LinhVucHoatDong = mAcc.LinhVucHoatDong,
                LanhDao = mAcc.LanhDao,
                ChucDanhLanhDao = mAcc.ChucDanhLanhDao,
                SdtLanhDao = mAcc.SdtLanhDao,
                EmailLanhDao = mAcc.EmailLanhDao,
                DaiDienMarketing = mAcc.DaiDienMarketing,
                ChucNangMarketing = mAcc.ChucNangMarketing,
                SdtMarketing = mAcc.SdtMarketing,
                EmailMarketing = mAcc.EmailMarketing,
                DaiDienNhanSu = mAcc.DaiDienNhanSu,
                ChucDanhNhanSu = mAcc.ChucDanhNhanSu,
                SdtNhanSu = mAcc.SdtNhanSu,
                EmailNhanSu = mAcc.EmailNhanSu,
                DaiDienKeToan = mAcc.DaiDienKeToan,
                ChucDanhKeToan = mAcc.ChucDanhKeToan,
                SdtKeToan = mAcc.SdtKeToan,
                EmailKeToan = mAcc.EmailKeToan,
                Fanpage = mAcc.Fanpage,
                ThoiGianGiaNhap = mAcc.ThoiGianGiaNhap,
                KhuVuc = mAcc.KhuVuc,
                GhiChu = mAcc.GhiChu
            }).SingleOrDefault();

            memberAccountModels.RegionList = new SelectList(db.KHUVUCs, "IDKhuVuc", "TenKhuVuc", memberAccountModels.KhuVuc);
            return View(memberAccountModels);
        }

        [HttpPost]
        public ActionResult Edit(MemberAccountModels memberAccountModels)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                return RedirectToAction("ManageMember", "Member", new { area = " " });
            }

            var id = memberAccountModels.ID;
            using (db)
            {
                var accountdata = db.HOIVIENs.Where(acc => acc.ID.Equals(id)).FirstOrDefault();
                {
                    if (accountdata != null)
                    {
                        try
                        {
                            accountdata.MaSoThue = memberAccountModels.MaSoThue.Trim();
                            accountdata.TenTiengViet = memberAccountModels.TenTiengViet.Trim();
                            accountdata.TenTiengAnh = memberAccountModels.TenTiengAnh.Trim();
                            accountdata.TenVietTat = memberAccountModels.TenVietTat.Trim();
                            accountdata.NgayThanhLap = memberAccountModels.NgayThanhLap;
                            accountdata.Website = memberAccountModels.Website.Trim();
                            accountdata.SdtCongTy = memberAccountModels.SdtCongTy.Trim();
                            accountdata.EmailCongTy = memberAccountModels.EmailCongTy.Trim();
                            accountdata.DiaChiGiaoDich = memberAccountModels.DiaChiGiaoDich.Trim();
                            accountdata.DiaChiTrenChungTu = memberAccountModels.DiaChiTrenChungTu.Trim();
                            accountdata.SoLuongNhanVien = memberAccountModels.SoLuongNhanVien;
                            accountdata.SoLuongLapTrinhVien = memberAccountModels.SoLuongLapTrinhVien;
                            accountdata.ThiTruongNoiDia = memberAccountModels.ThiTruongNoiDia.Trim();
                            accountdata.ThiTruongQuocTe = memberAccountModels.ThiTruongQuocTe.Trim();
                            accountdata.LinhVucHoatDong = memberAccountModels.LinhVucHoatDong.Trim();
                            accountdata.LanhDao = memberAccountModels.LanhDao.Trim();
                            accountdata.ChucDanhLanhDao = memberAccountModels.ChucDanhLanhDao.Trim();
                            accountdata.SdtLanhDao = memberAccountModels.SdtLanhDao.Trim();
                            accountdata.EmailLanhDao = memberAccountModels.EmailLanhDao.Trim();
                            accountdata.DaiDienMarketing = memberAccountModels.DaiDienMarketing.Trim();
                            accountdata.ChucNangMarketing = memberAccountModels.ChucNangMarketing.Trim();
                            accountdata.SdtMarketing = memberAccountModels.SdtMarketing.Trim();
                            accountdata.EmailMarketing = memberAccountModels.EmailMarketing.Trim();
                            accountdata.DaiDienNhanSu = memberAccountModels.DaiDienNhanSu.Trim();
                            accountdata.ChucDanhNhanSu = memberAccountModels.ChucDanhNhanSu.Trim();
                            accountdata.SdtNhanSu = memberAccountModels.SdtNhanSu.Trim();
                            accountdata.EmailNhanSu = memberAccountModels.EmailNhanSu.Trim();
                            accountdata.DaiDienKeToan = memberAccountModels.DaiDienKeToan.Trim();
                            accountdata.ChucDanhKeToan = memberAccountModels.ChucDanhKeToan.Trim();
                            accountdata.SdtKeToan = memberAccountModels.SdtKeToan.Trim();
                            accountdata.EmailKeToan = memberAccountModels.EmailKeToan.Trim();
                            accountdata.Fanpage = memberAccountModels.Fanpage.Trim();
                            accountdata.ThoiGianGiaNhap = memberAccountModels.ThoiGianGiaNhap.Trim();
                            accountdata.KhuVuc = memberAccountModels.KhuVuc;
                            accountdata.GhiChu = memberAccountModels.GhiChu;

                            db.SaveChanges();
                            return RedirectToAction("ManageMember", "Member", new { area = " " });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            return View();
        }

        //public ActionResult DetailMember();

        #region support method
        #endregion
        [HttpGet]
        public ActionResult DetailMember(int id)
        {
            MemberAccountModels memberAccountModels = db.HOIVIENs.Where(mAcc => mAcc.ID.Equals(id)).Select(mAcc => new MemberAccountModels()
            {
                ID = mAcc.ID,
                MaSoThue = mAcc.MaSoThue,
                TenTiengViet = mAcc.TenTiengViet,
                TenTiengAnh = mAcc.TenTiengAnh,
                TenVietTat = mAcc.TenVietTat,
                NgayThanhLap = mAcc.NgayThanhLap,
                Website = mAcc.Website,
                SdtCongTy = mAcc.SdtCongTy,
                EmailCongTy = mAcc.EmailCongTy,
                DiaChiGiaoDich = mAcc.DiaChiGiaoDich,
                DiaChiTrenChungTu = mAcc.DiaChiTrenChungTu,
                SoLuongNhanVien = mAcc.SoLuongNhanVien,
                SoLuongLapTrinhVien = mAcc.SoLuongLapTrinhVien,
                ThiTruongNoiDia = mAcc.ThiTruongNoiDia,
                ThiTruongQuocTe = mAcc.ThiTruongQuocTe,
                LinhVucHoatDong = mAcc.LinhVucHoatDong,
                LanhDao = mAcc.LanhDao,
                ChucDanhLanhDao = mAcc.ChucDanhLanhDao,
                SdtLanhDao = mAcc.SdtLanhDao,
                EmailLanhDao = mAcc.EmailLanhDao,
                DaiDienMarketing = mAcc.DaiDienMarketing,
                ChucNangMarketing = mAcc.ChucNangMarketing,
                SdtMarketing = mAcc.SdtMarketing,
                EmailMarketing = mAcc.EmailMarketing,
                DaiDienNhanSu = mAcc.DaiDienNhanSu,
                ChucDanhNhanSu = mAcc.ChucDanhNhanSu,
                SdtNhanSu = mAcc.SdtNhanSu,
                EmailNhanSu = mAcc.EmailNhanSu,
                DaiDienKeToan = mAcc.DaiDienKeToan,
                ChucDanhKeToan = mAcc.ChucDanhKeToan,
                SdtKeToan = mAcc.SdtKeToan,
                EmailKeToan = mAcc.EmailKeToan,
                Fanpage = mAcc.Fanpage,
                ThoiGianGiaNhap = mAcc.ThoiGianGiaNhap,
                sKhuVuc = mAcc.KHUVUC1.TenKhuVuc,
                GhiChu = mAcc.GhiChu
            }).SingleOrDefault();
            return View(memberAccountModels);
        }

        [HttpPost]
        public ActionResult ImportExcel(FormCollection formCollection)
        {
            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                return RedirectToAction("ManageMember", "Member", new { area = " " });
            }

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
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
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
                            member.ThoiGianGiaNhap = workSheet.Cells[rowIterator, 34].Value.ToString();
                            member.KhuVuc = Convert.ToInt32(workSheet.Cells[rowIterator, 35].Value);
                            member.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 36].Value);
                            memberlist.Add(member);
                        }
                    }
                }
            }
            using (db)
            {
                try
                {
                    foreach (var item in memberlist)
                    {
                        db.HOIVIENs.Add(item);
                    }
                    db.SaveChanges();
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
            CheckRole();
            var memberAccount = db.HOIVIENs.Where(t => t.ID.Equals(id)).FirstOrDefault();

            if (memberAccount != null && currentRole != 2)
            {
                db.HOIVIENs.Remove(memberAccount);
                db.SaveChanges();
            }
            else if (currentRole == 2)
            {
                ViewBag.Message = "Không thể xóa với tài khoản quản lí";
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