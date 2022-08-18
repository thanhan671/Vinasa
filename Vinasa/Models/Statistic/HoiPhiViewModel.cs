using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vinasa.Models
{
    public class HoiPhiViewModel
    {
        public string MaSoThue { get; set; }  
        
        public string TenCongTy { get; set; }
        
        public decimal TongThu { get; set; }

        public decimal DaDong { get; set; }

        public decimal ConLai { get; set; }  

    }
}