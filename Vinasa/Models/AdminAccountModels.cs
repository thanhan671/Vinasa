using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Vinasa.Validation;
using System;

namespace Vinasa.Models
{
    public class AdminAccountModels
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public string Ten { get; set; }

        public string Email { get; set; }
        public int Quyen { get; set; }
        public int TrangThai { get; set; }
        public string Sdt { get; set; }
        public string PhongBan { get; set; }

        [MinLength(6, ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        public string MatKhau { get; set; }

        [MinLength(6, ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        public string newMatKhau { get; set; }

        [MinLength(6, ErrorMessage = "Mật khẩu phải trên 6 ký tự")]
        public string reMatKhau { get; set; }

        public virtual QUYEN QUYEN1 { get; set; }
        public virtual TRANGTHAI TRANGTHAI1 { get; set; }

        public bool isEditMode { get; set; }

        [NotMapped]
        public SelectList RoleList { get; set; }
        [NotMapped]
        public SelectList StatusList { get; set; }

        public string ChucDanh { get; set; }
    }
}