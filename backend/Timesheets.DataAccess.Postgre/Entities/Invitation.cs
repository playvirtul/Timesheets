using Timesheets.Domain;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Invitation
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public Position Position { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}