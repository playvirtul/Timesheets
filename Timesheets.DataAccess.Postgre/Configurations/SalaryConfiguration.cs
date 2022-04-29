using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.HasKey(s => s.Position);

            builder.Property(s => s.MonthSalary).IsRequired();

            builder.Property(s => s.MonthBonus).IsRequired();

            builder.Property(s => s.SalaryPerHour).IsRequired();
        }
    }
}