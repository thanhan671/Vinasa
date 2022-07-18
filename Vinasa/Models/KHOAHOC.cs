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
        public string TenKhoaDaoTao { get; set; }
        public System.DateTime NgayBatDau { get; set; }
        public System.DateTime NgayKetThuc { get; set; }
        public string HinhThuc { get; set; }
    
        public virtual ICollection<THAMGIAKHOAHOC> THAMGIAKHOAHOCs { get; set; }
    }
}
