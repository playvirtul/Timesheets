using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class WorkTimeConfiguration : IEntityTypeConfiguration<WorkTime>
    {
        public void Configure(EntityTypeBuilder<WorkTime> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.EmployeeId).IsRequired();

            builder.Property(w => w.Hours).IsRequired();

            builder.Property(w => w.Date).IsRequired();

            builder.HasOne(w => w.Employee)
                 .WithMany(e => e.WorkTimes)
                 .HasForeignKey(w => w.EmployeeId);

            builder.HasOne(w => w.Project)
                 .WithMany(p => p.WorkTimes)
                 .HasForeignKey(w => w.ProjectId);
        }
    }
}