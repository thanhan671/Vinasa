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
    public class NGUOINHANGIAITHUONG
    {
        public int Id { get; set; }
        public int? GiaiThuongId { get; set; }

        [Display(Name = "Mã số thuế")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Không hợp lệ")]
        public string MaSoThue { get; set; }

        [Display(Name = "Tên đơn vị")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string TenDonVi { get; set; }

        [Display(Name = "Tên người đại diện pháp luật ")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string TenNguoiDaiDienPhapLuat { get; set; }

        [Display(Name = "Chức danh")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string ChucDanh { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Di động")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không hợp lệ")]
        public string DiDong { get; set; }

        [Display(Name = "Tên người liên hệ với BTC")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string TenNguoiLienHeVoiBTC { get; set; }

        [Display(Name = "Chức danh người liên hệ")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string ChucDanhNguoiLienHe { get; set; }

        [Display(Name = "Email người liên hệ")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Không hợp lệ")]
        public string EmailNguoiLienHe { get; set; }

        [Display(Name = "Di động người liên hệ")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không hợp lệ")]

        public string DiDongNguoiLienHe { get; set; }

        [Display(Name = "Tỉnh thành")]
        public int ProvinceId { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string DiaChi { get; set; }

        [Display(Name = "Phiếu đăng ký (đính kèm file hoặc link URL)")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string PhieuDangKy { get; set; }

        [Display(Name = "Kinh phí truyền thông")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Vui lòng không nhập số âm")]
        public int KinhPhiTruyenThong { get; set; }

        [Display(Name = "Giải thưởng")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string GiaiThuong { get; set; }

        [NotMapped]
        public SelectList Provinces { get; set; }
        [NotMapped]
        public GIAITHUONG GiaiThuongs { get; set; }
    }
}