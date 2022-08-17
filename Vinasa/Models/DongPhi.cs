using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.DAL;
using Vinasa.Validation;

namespace Vinasa.Models
{
    
    public partial class DongPhi
    {
        public int ID { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTy { get; set; }
        public string NguoiNhanPhieuThu { get; set; }
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayChuyenTien { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayGuiPhieuThu { get; set; }
        public decimal SoTienDong { get; set; }
        public string GhiChu { get; set; }
    }
}
