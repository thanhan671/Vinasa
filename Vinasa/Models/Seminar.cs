using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.Validation;

namespace Vinasa.Models
{
    public class Seminar: IValidatableObject
    {
        public Seminar()
        {
            SeminarParticipants = new List<SeminarParticipant>();
        }

        public int Id { get; set; }

        [Display(Name = "Tên Hội Nghị")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [SeminarRequired(ErrorMessage = "Đã tồn tại hội nghị!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Title { get; set; }

        
        [Display(Name = "Thời Gian Diễn Ra")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DateTimeRequired(ErrorMessage = "Thời gian diễn ra phải lớn hơn ngày hiện tại!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public DateTime OpenDate { get; set; }

        [Display(Name = "Thời Gian Kết Thúc")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DateTimeRequired(ErrorMessage = "Thời gian kết thúc phải lớn hơn ngày hiện tại!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        public DateTime CloseDate { get; set; }

        [Display(Name = "Địa Điểm Diễn Ra")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedUtc { get; set; }

        public List<SeminarParticipant> SeminarParticipants { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (CloseDate < OpenDate)
            {
                yield return new ValidationResult("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian diễn ra");
            }
        }
    }
}