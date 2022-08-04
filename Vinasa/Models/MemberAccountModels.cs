using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Vinasa.Models
{
    public class MemberAccountModels
    {
        public int ID { get; set; }
        public string MaSoThue { get; set; }
        public string TenTiengViet { get; set; }
        public string TenTiengAnh { get; set; }
        public string TenVietTat { get; set; }
        public Nullable<System.DateTime> NgayThanhLap { get; set; }
        public string Website { get; set; }
        public string SdtCongTy { get; set; }
        public string EmailCongTy { get; set; }
        public string DiaChiGiaoDich { get; set; }
        public string DiaChiTrenChungTu { get; set; }
        public int SoLuongNhanVien { get; set; }
        public int SoLuongLapTrinhVien { get; set; }
        public string ThiTruongNoiDia { get; set; }
        public string ThiTruongQuocTe { get; set; }
        public string LinhVucHoatDong { get; set; }
        public string LanhDao { get; set; }
        public string ChucDanhLanhDao { get; set; }
        public string SdtLanhDao { get; set; }
        public string EmailLanhDao { get; set; }
        public string DaiDienMarketing { get; set; }
        public string ChucNangMarketing { get; set; }
        public string SdtMarketing { get; set; }
        public string EmailMarketing { get; set; }
        public string DaiDienNhanSu { get; set; }
        public string ChucDanhNhanSu { get; set; }
        public string SdtNhanSu { get; set; }
        public string EmailNhanSu { get; set; }
        public string DaiDienKeToan { get; set; }
        public string ChucDanhKeToan { get; set; }
        public string SdtKeToan { get; set; }
        public string EmailKeToan { get; set; }
        public string Fanpage { get; set; }
        public string ThoiGianGiaNhap { get; set; }
        public int KhuVuc { get; set; }

        public string GhiChu { get; set; }

        public string sKhuVuc { get; set; }
        public virtual KHUVUC KHUVUC1 { get; set; }

        public SelectList RegionList { get; set; }
    }
}