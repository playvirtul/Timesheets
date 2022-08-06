using Timesheets.Domain.Auth;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? TelegramUserName { get; set; }

        public string? PasswordHash { get; set; }

        public Role Role { get; set; }
    }
}