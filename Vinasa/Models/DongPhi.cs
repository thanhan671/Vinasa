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

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string MaSoThue { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string TenCongTy { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string NguoiNhanPhieuThu { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không hợp lệ")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayChuyenTien { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayGuiPhieuThu { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public decimal SoTienDong { get; set; }
        public string GhiChu { get; set; }
    }
}
