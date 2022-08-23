using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.DAL;
using Vinasa.Validation;

namespace Vinasa.Models
{
    public class Seminar : IValidatableObject
    {
        public Seminar()
        {
            SeminarParticipants = new List<SeminarParticipant>();
        }

        public int Id { get; set; }

        [Display(Name = "Tên Hội Nghị")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Title { get; set; }


        [Display(Name = "Thời Gian Diễn Ra")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OpenDate { get; set; }

        [Display(Name = "Thời Gian Kết Thúc")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
            SeminarContext _db = new SeminarContext();
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (CloseDate < OpenDate)
                validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian diễn ra", new[] { "CloseDate" }));

            if (_db.Seminars.Any(it => it.Title == Title && it.Id != Id))
                validationResult.Add(new ValidationResult("Đã tồn tại hội nghị!", new[] { "Title" }));

            var seminar = _db.Seminars.FirstOrDefault(it => it.Id == Id);
            if (seminar == null)
            {
                if (CloseDate <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian kết thúc phải lớn hơn ngày hiện tại!", new[] { "CloseDate" }));
                if (OpenDate <= DateTime.Now)
                    validationResult.Add(new ValidationResult("Thời gian diễn ra phải lớn hơn ngày hiện tại!", new[] { "OpenDate" }));
            }
            return validationResult;
        }
    }
}