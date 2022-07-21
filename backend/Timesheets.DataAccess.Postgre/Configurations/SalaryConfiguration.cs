 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasKey(s => s.EmployeeId);

            builder.Property(s => s.EmployeeId).IsRequired();

            builder.Property(s => s.Amount).IsRequired();

            builder.Property(s => s.Bonus).IsRequired();

            builder.Property(s => s.SalaryType).IsRequired();

            builder.HasOne(s => s.Employee)
                .WithOne()
                .HasForeignKey<Salary>(s => s.EmployeeId);
        }
    }
}