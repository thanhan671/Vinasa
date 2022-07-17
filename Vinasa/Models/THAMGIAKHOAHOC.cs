using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Validation;

namespace Vinasa.Models
{
    public class THAMGIAKHOAHOC
    {
        public int Id { get; set; }

        public int IdKhoaHoc { get; set; }

        public string HoTen { get; set; }

        public string CongTyToChucCoQuan { get; set; }

        public string ChucDanh { get; set; }

        public string Email { get; set; }

        public string Sdt { get; set; }

        public int SoLuongHocVien { get; set; }

        public bool HoiVienVinasa { get; set; }
    }
}