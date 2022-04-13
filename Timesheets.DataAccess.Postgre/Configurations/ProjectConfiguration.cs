using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.DataAccess.Postgre
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.WorkTimes);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}