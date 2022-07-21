using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.Code).IsUnique();

            builder.Property(i => i.FirstName).IsRequired();

            builder.Property(i => i.LastName).IsRequired();

            builder.Property(i => i.Position).IsRequired();
        }
    }
}