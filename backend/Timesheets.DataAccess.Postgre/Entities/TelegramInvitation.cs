using Timesheets.Domain;
using Timesheets.Domain.Auth;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class TelegramInvitation
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public Position Position { get; set; }

        public Role Role { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }
    }
}