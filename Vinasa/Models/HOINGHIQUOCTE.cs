using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.DAL;
using Vinasa.Validation;

namespace Vinasa.Models
{
    public partial class HOINGHIQUOCTE : IValidatableObject
    {
        public HOINGHIQUOCTE()
        {
            this.THAMGIAHOINGHIQUOCTEs = new HashSet<THAMGIAHOINGHIQUOCTE>();
        }
    
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string DiaDiem { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ThoiGianBatDau { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ThoiGianKetThuc { get; set; }
    
        public virtual ICollection<THAMGIAHOINGHIQUOCTE> THAMGIAHOINGHIQUOCTEs { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            SEP25Team16Entities2 _db = new SEP25Team16Entities2();
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (ThoiGianKetThuc < ThoiGianBatDau)
                validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu", new[] { "ThoiGianKetThuc" }));

            if (_db.HOINGHIQUOCTEs.Any(it => it.Ten == Ten && it.ID != ID))
                validationResult.Add(new ValidationResult("Đã tồn tại hội nghị này!", new[] { "Ten" }));

            var hoiNghiQT = _db.HOINGHIQUOCTEs.FirstOrDefault(it => it.ID == ID);
            if (hoiNghiQT == null)
            {
                if (ThoiGianBatDau <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian diễn ra phải lớn hơn ngày hiện tại!", new[] { "ThoiGianBatDau" }));
                if (ThoiGianKetThuc <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn ngày hiện tại!", new[] { "ThoiGianKetThuc" }));

            }
            return validationResult;
        }
    }
}
