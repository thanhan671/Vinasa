//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vinasa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KHUVUC
    {
        public KHUVUC()
        {
            this.HOIVIENs = new HashSet<HOIVIEN>();
        }
    
        public int IDKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
    
        public virtual ICollection<HOIVIEN> HOIVIENs { get; set; }
    }
}