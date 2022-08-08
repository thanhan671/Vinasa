using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vinasa.Models
{
    public class HoiPhi
    {
        public int Id { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTy { get; set; }
        public string DiaChiGhiPhieuThu { get; set; }
        public string DiaChiGuiPhieuThu { get; set; }
        public string NguoiNhanPhieu { get; set; }

        public int DienThoai { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int HoiPhiNamTruoc { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int HoiPhiNamSau { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int TongThu { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int DaDong { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int ConLai { get; set; }

        public DateTime? NgayChuyenTien { get; set; }
        public DateTime? NgayGuiPhieuThu { get; set; }
        public string GhiChu { get; set; }
    }
}