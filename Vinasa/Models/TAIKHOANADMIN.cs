using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Vinasa.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vinasa.Models
{
    public partial class TAIKHOANADMIN : IValidatableObject
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Không hợp lệ")]
        public string Email { get; set; }
        public int Quyen { get; set; }
        public int TrangThai { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không hợp lệ")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string PhongBan { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string ChucDanh { get; set; }

        [MinLength(6, ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        public string matKhauMoi { get; set; }

        [MinLength(6, ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        public string xacNhanMatKhau { get; set; }

        [NotMapped]
        public SelectList RoleList { get; set; }
        [NotMapped]
        public SelectList StatusList { get; set; }

        public virtual QUYEN QUYEN1 { get; set; }
        public virtual TRANGTHAI TRANGTHAI1 { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            SEP25Team16Entities2 _db = new SEP25Team16Entities2();
            List<ValidationResult> validationResult = new List<ValidationResult>();

            var taiKhoanAdmin = _db.TAIKHOANADMINs.FirstOrDefault(a => a.ID == ID);
            if (taiKhoanAdmin == null)
            {
                if (!MatKhau.Equals(xacNhanMatKhau))
                    validationResult.Add(new ValidationResult("Xác nhận mật khẩu không giống", new[] { "xacNhanMatKhau" }));
            }
            if (taiKhoanAdmin != null && matKhauMoi != null)
            {
                if (!matKhauMoi.Equals(xacNhanMatKhau))
                    validationResult.Add(new ValidationResult("Xác nhận mật khẩu không giống", new[] { "xacNhanMatKhau" }));
            }
            return validationResult;
        }
    }
}