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
    public class SeminarParticipant
    {
        public int Id { get; set; }

        public int? SeminarId { get; set; }

        [Display(Name = "Họ và tên người tham dự")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Name { get; set; }

        [Display(Name = "Mã số thuế")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string TaxNumber { get; set; }

        [Display(Name = "Tên đơn vị")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Company { get; set; }

        [Display(Name = "Chức danh")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Position { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string Email { get; set; }

        [Display(Name = "Di động")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tỉnh thành")]
        public int ProvinceId { get; set; }

        [Display(Name = "Đơn vị chúng tôi là")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        [StringRequired(ErrorMessage = "Vui lòng điền trường này!")]
        public string JobTitle { get; set; }

        [Display(Name = "Lĩnh vực hoạt động")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public string Operation { get; set; }

        [Display(Name = "Đăng ký hội thảo")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public bool RegistrySeminar { get; set; }

        [Display(Name = "Đăng ký Business Matching")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public bool RegistryBusinessMatching { get; set; }

        [Display(Name = "Đăng ký gian hàng triển lãm")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public bool RegistryExhibition { get; set; }

        [Display(Name = "Đăng ký vé tham dự")]
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public bool RegistryTicket { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedUtc { get; set; }

        [NotMapped]
        public SelectList Provinces { get; set; }
        [NotMapped]
        public Seminar Seminar { get; set; }
    }

}