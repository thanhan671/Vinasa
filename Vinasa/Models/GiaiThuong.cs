﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.DAL;
using Vinasa.Validation;

namespace Vinasa.Models
{
    public class GIAITHUONG : IValidatableObject
    {
        public GIAITHUONG()
        {
            NguoiNhanGiaiThuong = new List<NGUOINHANGIAITHUONG>();
        }
        public int Id { get; set; }

        [Display(Name = "Tên Giải Thưởng")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Title { get; set; }


        [Display(Name = "Thời Gian Diễn Ra")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OpenDate { get; set; }

        [Display(Name = "Địa Điểm Diễn Ra")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedUtc { get; set; }

        public List<NGUOINHANGIAITHUONG> NguoiNhanGiaiThuong { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            SeminarContext _db = new SeminarContext();
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (_db.GIAITHUONG.Any(it => it.Title == Title && it.Id != Id))
                validationResult.Add(new ValidationResult("Đã tồn tại giải thưởng!", new[] { "Title" }));

            var giaiThuong = _db.GIAITHUONG.FirstOrDefault(it => it.Id == Id);
            if (giaiThuong == null)
            {
                if (OpenDate <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian diễn ra phải lớn hơn ngày hiện tại!", new[] { "OpenDate" }));
            }
            return validationResult;
        }
    }
}