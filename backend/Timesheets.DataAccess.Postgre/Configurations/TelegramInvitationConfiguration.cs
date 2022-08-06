using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class TelegramInvitationConfiguration : IEntityTypeConfiguration<TelegramInvitation>
    {
        public void Configure(EntityTypeBuilder<TelegramInvitation> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.Code).IsUnique();

            builder.Property(i => i.UserName).IsRequired();

            builder.Property(i => i.FirstName).IsRequired().HasMaxLength(Domain.Employee.MAX_FIRSTNAME_LENGTH);

            builder.Property(i => i.LastName).IsRequired().HasMaxLength(Domain.Employee.MAX_LASTNAME_LENGTH);

            builder.Property(i => i.Position).IsRequired();
        }
    }
}