using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class MemberController : Controller
    {

        SEP25Team16Entities2 db = new SEP25Team16Entities2();

        private int memberID;
        // GET: Member
        [HttpGet]
        public ActionResult Index()
        {
            var data = db.HOIVIENs.ToList();
            ViewBag.hoivienDetail = data;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int memberID)
        {

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
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
                KhuVuc = mAcc.KhuVuc
            }).SingleOrDefault();

            var abc = db.KHUVUCs.Select(k => new KhuVucModels { 
            IDKhuVuc=k.IDKhuVuc,
            TenKhuVuc=k.TenKhuVuc
            }).ToList();

            Session["khuvuc"] = abc;

            return View(memberAccountModels);
        }

        [HttpPost]
        public ActionResult Edit(MemberAccountModels memberAccountModels)
        {
            var MaSoThue = memberAccountModels.MaSoThue.Trim();
            var TenTiengViet = memberAccountModels.TenTiengViet.Trim();
            var TenTiengAnh = memberAccountModels.TenTiengAnh.Trim();
            var TenVietTat = memberAccountModels.TenVietTat.Trim();
            var NgayThanhLap = memberAccountModels.NgayThanhLap.Trim();
            var Website = memberAccountModels.Website.Trim();
            var SdtCongTy = memberAccountModels.SdtCongTy.Trim();
            var EmailCongTy = memberAccountModels.EmailCongTy.Trim();
            var DiaChiGiaoDich = memberAccountModels.DiaChiGiaoDich.Trim();
            var DiaChiTrenChungTu = memberAccountModels.DiaChiTrenChungTu.Trim();
            var SoLuongNhanVien = memberAccountModels.SoLuongNhanVien.ToString().Trim();
            var SoLuongLapTrinhVien = memberAccountModels.SoLuongLapTrinhVien.ToString().Trim();
            var ThiTruongNoiDia = memberAccountModels.ThiTruongNoiDia.Trim();
            var ThiTruongQuocTe = memberAccountModels.ThiTruongQuocTe.Trim();
            var LinhVucHoatDong = memberAccountModels.LinhVucHoatDong.Trim();
            var LanhDao = memberAccountModels.LanhDao.Trim();
            var ChucDanhLanhDao = memberAccountModels.ChucDanhLanhDao.Trim();
            var SdtLanhDao = memberAccountModels.SdtLanhDao.Trim();
            var EmailLanhDao = memberAccountModels.EmailLanhDao.Trim();
            var DaiDienMarketing = memberAccountModels.DaiDienMarketing.Trim();
            var ChucNangMarketing = memberAccountModels.ChucNangMarketing.Trim();
            var SdtMarketing = memberAccountModels.SdtMarketing.Trim();
            var EmailMarketing = memberAccountModels.EmailMarketing.Trim();
            var DaiDienNhanSu = memberAccountModels.DaiDienNhanSu.Trim();
            var ChucDanhNhanSu = memberAccountModels.ChucDanhNhanSu.Trim();
            var SdtNhanSu = memberAccountModels.SdtNhanSu.Trim();
            var EmailNhanSu = memberAccountModels.EmailNhanSu.Trim();
            var DaiDienKeToan = memberAccountModels.DaiDienKeToan.Trim();
            var ChucDanhKeToan = memberAccountModels.ChucDanhKeToan.Trim();
            var SdtKeToan = memberAccountModels.SdtKeToan.Trim();
            var EmailKeToan = memberAccountModels.EmailKeToan.Trim();
            var Fanpage = memberAccountModels.Fanpage.Trim();
            var ThoiGianGiaNhap = memberAccountModels.ThoiGianGiaNhap.Trim();
            return Content(memberAccountModels.ID.ToString());
        }

        #region support method
        private int FindRowID()
        {

            return memberID;
        }
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
                ThoiGianGiaNhap = mAcc.ThoiGianGiaNhap
            }).SingleOrDefault();
            var abc = db.KHUVUCs.Select(k => new KhuVucModels
            {
                IDKhuVuc = k.IDKhuVuc,
                TenKhuVuc = k.TenKhuVuc
            }).ToList();

            Session["khuvuc"] = abc;

            return View(memberAccountModels);

            return View(memberAccountModels);
        }

        [HttpPost]
        public ActionResult DetailMember(MemberAccountModels memberAccountModels)
        {
            var MaSoThue = memberAccountModels.MaSoThue.Trim();
            var TenTiengViet = memberAccountModels.TenTiengViet.Trim();
            var TenTiengAnh = memberAccountModels.TenTiengAnh.Trim();
            var TenVietTat = memberAccountModels.TenVietTat.Trim();
            var NgayThanhLap = memberAccountModels.NgayThanhLap.Trim();
            var Website = memberAccountModels.Website.Trim();
            var SdtCongTy = memberAccountModels.SdtCongTy.Trim();
            var EmailCongTy = memberAccountModels.EmailCongTy.Trim();
            var DiaChiGiaoDich = memberAccountModels.DiaChiGiaoDich.Trim();
            var DiaChiTrenChungTu = memberAccountModels.DiaChiTrenChungTu.Trim();
            var SoLuongNhanVien = memberAccountModels.SoLuongNhanVien.ToString().Trim();
            var SoLuongLapTrinhVien = memberAccountModels.SoLuongLapTrinhVien.ToString().Trim();
            var ThiTruongNoiDia = memberAccountModels.ThiTruongNoiDia.Trim();
            var ThiTruongQuocTe = memberAccountModels.ThiTruongQuocTe.Trim();
            var LinhVucHoatDong = memberAccountModels.LinhVucHoatDong.Trim();
            var LanhDao = memberAccountModels.LanhDao.Trim();
            var ChucDanhLanhDao = memberAccountModels.ChucDanhLanhDao.Trim();
            var SdtLanhDao = memberAccountModels.SdtLanhDao.Trim();
            var EmailLanhDao = memberAccountModels.EmailLanhDao.Trim();
            var DaiDienMarketing = memberAccountModels.DaiDienMarketing.Trim();
            var ChucNangMarketing = memberAccountModels.ChucNangMarketing.Trim();
            var SdtMarketing = memberAccountModels.SdtMarketing.Trim();
            var EmailMarketing = memberAccountModels.EmailMarketing.Trim();
            var DaiDienNhanSu = memberAccountModels.DaiDienNhanSu.Trim();
            var ChucDanhNhanSu = memberAccountModels.ChucDanhNhanSu.Trim();
            var SdtNhanSu = memberAccountModels.SdtNhanSu.Trim();
            var EmailNhanSu = memberAccountModels.EmailNhanSu.Trim();
            var DaiDienKeToan = memberAccountModels.DaiDienKeToan.Trim();
            var ChucDanhKeToan = memberAccountModels.ChucDanhKeToan.Trim();
            var SdtKeToan = memberAccountModels.SdtKeToan.Trim();
            var EmailKeToan = memberAccountModels.EmailKeToan.Trim();
            var Fanpage = memberAccountModels.Fanpage.Trim();
            var ThoiGianGiaNhap = memberAccountModels.ThoiGianGiaNhap.Trim();
            return Content(memberAccountModels.ID.ToString());
        }

    }
}