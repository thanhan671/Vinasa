using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Vinasa.Models;

namespace Vinasa.DAL
{
    public class CourseContext : DbContext
    {
        public CourseContext() : base("CourseContext")
        {}

        //public DbSet<KHOAHOC> KHOAHOCs { get; set; }
        //public DbSet<SeminarParticipant> SeminarParticipants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<KHOAHOC>().ToTable("KHOAHOCs");
        }
    }
}