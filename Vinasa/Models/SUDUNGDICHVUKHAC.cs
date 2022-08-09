﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vinasa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using Vinasa.Validation;

    public partial class SUDUNGDICHVUKHAC : IValidatableObject
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Không hợp lệ")]
        public string MaSoThue { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string TenCongTy { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayBatDauHopDong { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayKetThucHopDong { get; set; }
        public string TenDichVuKhac { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int GiaGoc { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public int GiaUuDai { get; set; }
        public string ChiecKhauVinasa { get; set; }
        public string TenNguoiLienHe { get; set; }
        public string ChucDanh { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không hợp lệ")]
        public string DienThoai { get; set; }
        public string GhiChu { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            SEP25Team16Entities2 _db = new SEP25Team16Entities2();
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (NgayKetThucHopDong < NgayBatDauHopDong)
                validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu", new[] { "NgayKetThucHopDong" }));

            var dichVuKetNoi = _db.SUDUNGDICHVUKETNOIs.FirstOrDefault(it => it.ID == ID);
            if (dichVuKetNoi == null)
            {
                if (NgayBatDauHopDong <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian diễn ra phải lớn hơn ngày hiện tại!", new[] { "NgayBatDauHopDong" }));
                if (NgayKetThucHopDong <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn ngày hiện tại!", new[] { "NgayKetThucHopDong" }));

            }
            return validationResult;
        }
    }
}
