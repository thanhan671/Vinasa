using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.Validation;

namespace Vinasa.Models
{

    public partial class KHOAHOC : IValidatableObject
    {
        public KHOAHOC()
        {
            this.THAMGIAKHOAHOCs = new List<THAMGIAKHOAHOC>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public string TenKhoaDaoTao { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        //[DateTimeRequired(ErrorMessage = "Thời gian diễn ra phải lớn hơn ngày hiện tại!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        //[EndDateTimeRequired("NgayBatDau")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayKetThuc { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public string HinhThuc { get; set; }

        public virtual ICollection<THAMGIAKHOAHOC> THAMGIAKHOAHOCs { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            SEP25Team16Entities2 _db = new SEP25Team16Entities2();
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (NgayKetThuc < NgayBatDau)
                validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu", new[] { "NgayKetThuc" }));

            if (_db.KHOAHOCs.Any(it => it.TenKhoaDaoTao == TenKhoaDaoTao && it.Id != Id))
                validationResult.Add(new ValidationResult("Đã tồn tại khóa đào tạo này!", new[] { "TenKhoaDaoTao" }));

            var khoaDaoTao = _db.KHOAHOCs.FirstOrDefault(it => it.Id == Id);
            if (khoaDaoTao == null)
            {
                if (NgayBatDau <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian diễn ra phải lớn hơn ngày hiện tại!", new[] { "NgayBatDau" }));
                if (NgayKetThuc <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn ngày hiện tại!", new[] { "NgayKetThuc" }));

            }
            return validationResult;
        }
    }
}