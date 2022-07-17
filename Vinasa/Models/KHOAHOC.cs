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
    public class KHOAHOC
    {
        public int Id { get; set; }

        public string MaSoThue { get; set; }

        public string TenKhoaDaotao { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public string HinhThuc { get; set; }

    }
}