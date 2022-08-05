using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.DataAccess.Postgre.Configurations
{
    public class TelegramUserConfiguration : IEntityTypeConfiguration<TelegramUser>
    {
        public void Configure(EntityTypeBuilder<TelegramUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.UserName).IsRequired();

            builder.Property(u => u.ChatId).IsRequired();
        }
    }
}