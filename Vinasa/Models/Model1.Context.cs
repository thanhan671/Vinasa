﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public partial class SEP25Team16Entities2 : DbContext
    {
        public SEP25Team16Entities2()
            : base("name=SEP25Team16Entities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<KHUVUC> KHUVUCs { get; set; }
        public DbSet<QUYEN> QUYENs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TRANGTHAI> TRANGTHAIs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<SeminarParticipant> SeminarParticipants { get; set; }
        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<KHOAHOC> KHOAHOCs { get; set; }
        public DbSet<THAMGIAKHOAHOC> THAMGIAKHOAHOCs { get; set; }
        public DbSet<HOINGHIQUOCTE> HOINGHIQUOCTEs { get; set; }
        public DbSet<THAMGIAHOINGHIQUOCTE> THAMGIAHOINGHIQUOCTEs { get; set; }
        public DbSet<GIAITHUONG> GIAITHUONGs { get; set; }
        public DbSet<NGUOINHANGIAITHUONG> NGUOINHANGIAITHUONGs { get; set; }
        public DbSet<TAIKHOANADMIN> TAIKHOANADMINs { get; set; }
        public DbSet<HOIVIEN> HOIVIENs { get; set; }

        //public DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<SUDUNGDICHVUKETNOI> SUDUNGDICHVUKETNOIs { get; set; }
    
    }
}
