using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Vinasa.Models
{
    public class AdminAccountModels
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public int Quyen { get; set; }
        public int TrangThai { get; set; }

        public string Sdt { get; set; }
        public string PhongBan { get; set; }
        public string MatKhau { get; set; }

        public string newMatKhau { get; set; }
        public string reMatKhau { get; set; }
        public string sQuyen { get; set; }
        public string sTrangThai { get; set; }
        [Required(ErrorMessage = "Vui lòng điền trường này!")]
        public virtual QUYEN QUYEN1 { get; set; }
        public virtual TRANGTHAI TRANGTHAI1 { get; set; }

        public bool isEditMode { get; set; }
        
        [NotMapped]
        public SelectList RoleList { get; set; }
        [NotMapped]
        public SelectList StatusList { get; set; }
    }
}