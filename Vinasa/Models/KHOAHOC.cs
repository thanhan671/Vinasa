using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.Validation;

namespace Vinasa.Models
{
    
    public partial class KHOAHOC
    {
        public KHOAHOC()
        {
            this.THAMGIAKHOAHOCs = new List<THAMGIAKHOAHOC>();
        }
    
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        //[CourseRequired(ErrorMessage = "Đã tồn tại khóa học này!")]
        public string TenKhoaDaoTao { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [DateTimeRequired(ErrorMessage = "Thời gian diễn ra phải lớn hơn ngày hiện tại!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        //[DateTimeRequired(ErrorMessage = "Thời gian diễn ra phải lớn hơn ngày bắt đầu!")]
        [EndDateTimeRequired("NgayBatDau")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime NgayKetThuc { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public string HinhThuc { get; set; }

        //public bool isEdit { get; set; }
    
        public virtual ICollection<THAMGIAKHOAHOC> THAMGIAKHOAHOCs { get; set; }
    }
}
