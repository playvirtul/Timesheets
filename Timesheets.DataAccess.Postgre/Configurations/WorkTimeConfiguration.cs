using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timesheets.DataAccess.Postgre
{
    public class WorkTimeConfiguration : IEntityTypeConfiguration<WorkTime>
    {
        public void Configure(EntityTypeBuilder<WorkTime> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}