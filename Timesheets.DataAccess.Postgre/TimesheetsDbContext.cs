using Microsoft.EntityFrameworkCore;
using Timesheets.DataAccess.Postgre.Configurations;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre
{
    public class TimesheetsDbContext : DbContext
    {
        public TimesheetsDbContext(DbContextOptions<TimesheetsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkTime> WorkTimes { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new WorkTimeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}