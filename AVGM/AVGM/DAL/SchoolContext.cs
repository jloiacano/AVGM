using System;
using AVGM.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVGM.DAL
{
    public class SchoolContext : DbContext
    {

        string databaseDrop = System.Configuration.ConfigurationManager.AppSettings["DatabaseDrop"];

        public SchoolContext() : base("name=SchoolContext")
        {
            if (databaseDrop == "drop")
            {
                Database.SetInitializer<SchoolContext>(new DropCreateDatabaseAlways<SchoolContext>());
            }
            else if (databaseDrop == "recreateOnChange")
            {
                Database.SetInitializer<SchoolContext>(new DropCreateDatabaseIfModelChanges<SchoolContext>());
            }
            else
            {
                Database.SetInitializer<SchoolContext>(new CreateDatabaseIfNotExists<SchoolContext>());
            }

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<StudentGuardian> StudentGuardians { get; set; }
        //public DbSet<GuardianAddress> GuardianAddresses { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}