using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Vinasa.Models;

namespace Vinasa.DAL
{
    public class SeminarContext : DbContext 
    {
        public SeminarContext() : base("SeminarContext")
        {

        }

        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<SeminarParticipant> SeminarParticipants { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<GIAITHUONG> GIAITHUONG { get; set; }
        public DbSet<NGUOINHANGIAITHUONG> NGUOINHANGIAITHUONG { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Seminar>().ToTable("Seminars");
            modelBuilder.Entity<SeminarParticipant>().ToTable("SeminarParticipants");
            modelBuilder.Entity<Province>().ToTable("Provinces");
            modelBuilder.Entity<GIAITHUONG>().ToTable("GIAITHUONG");
            modelBuilder.Entity<NGUOINHANGIAITHUONG>().ToTable("NGUOINHANGIAITHUONG");
        }

    }
}