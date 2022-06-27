using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vinasa.Models;

namespace Vinasa.DAL
{
    public class SeminarContext : DbContext 
    {
        public SeminarContext() : base("SchoolContext")
        {

        }

        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<SeminarParticipant> SeminarParticipants { get; set; }
        public DbSet<Province> Provinces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}