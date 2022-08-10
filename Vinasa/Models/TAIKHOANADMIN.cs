using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Vinasa.Validation;
using System;

namespace Vinasa.Models
{

    
    public partial class TAIKHOANADMIN
    {
        public int ID { get; set; }

        public string Ten { get; set; }

        public string Email { get; set; }
        public int Quyen { get; set; }
        public int TrangThai { get; set; }

        public string Sdt { get; set; }

        public string PhongBan { get; set; }

        public string MatKhau { get; set; }

        public string ChucDanh { get; set; }
    
        public virtual QUYEN QUYEN1 { get; set; }
        public virtual TRANGTHAI TRANGTHAI1 { get; set; }
    }
}
