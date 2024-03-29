﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vinasa.Models
{
    public class RoleModels
    {
        public RoleModels()
        {
            this.TAIKHOANADMINs = new HashSet<TAIKHOANADMIN>();
        }

        public int IDQuyen { get; set; }
        public string TenQuyen { get; set; }

        public virtual ICollection<TAIKHOANADMIN> TAIKHOANADMINs { get; set; }
    }

}