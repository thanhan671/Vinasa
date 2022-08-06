using System;
using System.Collections.Generic;
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
        public int HoiPhiNamTruoc { get; set; }   
        public int HoiPhiNamSau { get; set; }   
        public int TongThu { get; set; }   
        public int DaDong { get; set; }   
        public int ConLai { get; set; }   
        public DateTime? NgayChuyenTien { get; set; }   
        public DateTime? NgayGuiPhieuThu { get; set; }   
        public string GhiChu { get; set; }   

    }
}