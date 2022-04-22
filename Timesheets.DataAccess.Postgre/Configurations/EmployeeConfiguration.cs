using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();

            builder.HasMany(e => e.Projects)
                .WithMany(p => p.Employees);
        }
    }
}