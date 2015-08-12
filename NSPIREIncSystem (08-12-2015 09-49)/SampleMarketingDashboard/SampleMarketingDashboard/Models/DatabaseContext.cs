using System;
using System.Data.Entity;

namespace NSPIREIncSystem.Models
{
    class DatabaseContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<LeadActivity> LeadActivities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SalesStage> SalesStages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new Initializer());
            modelBuilder.Entity<UserAccount>().ToTable("UserAccounts", "public");
            modelBuilder.Entity<Employee>().ToTable("Employees", "public");
            modelBuilder.Entity<Lead>().ToTable("LeadDetails", "public");
            modelBuilder.Entity<Territory>().ToTable("Territories", "public");
            modelBuilder.Entity<LeadActivity>().ToTable("LeadActivities", "public");
            modelBuilder.Entity<Contact>().ToTable("Contacts", "public");
            modelBuilder.Entity<SalesStage>().ToTable("SalesStages", "public");
        }

        public class Initializer : IDatabaseInitializer<DatabaseContext>
        {
            public void InitializeDatabase(DatabaseContext context)
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                    Seed(context);
                    context.SaveChanges();
                }
            }

            private void Seed(DatabaseContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
