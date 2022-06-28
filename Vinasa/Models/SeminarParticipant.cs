using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vinasa.Models
{
    public class SeminarParticipant
    {
        public int Id { get; set; }

        public int? SeminarId { get; set; }

        [Display(Name = "Họ và tên người tham dự")]
        public string Name { get; set; }

        [Display(Name = "Mã số thuế")]
        public string TaxNumber { get; set; }

        [Display(Name = "Tên đơn vị")]
        public string Company { get; set; }

        [Display(Name = "Chức danh")]
        public string Position { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Di động")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tỉnh thành")]
        public int ProvinceId { get; set; }

        [Display(Name = "Đơn vị chúng tôi là")]
        public string JobTitle { get; set; }

        [Display(Name = "Lĩnh vực hoạt động")]
        public string Operation { get; set; }

        [Display(Name = "Đăng ký hội thảo")]
        public bool RegistrySeminar { get; set; }

        [Display(Name = "Đăng ký Business Matching")]
        public bool RegistryBusinessMatching { get; set; }

        [Display(Name = "Đăng ký gian hàng triển lãm")]
        public bool RegistryExhibition { get; set; }

        [Display(Name = "Đăng ký vé tham dự")]
        public bool RegistryTicket { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedUtc { get; set; }
    }

}