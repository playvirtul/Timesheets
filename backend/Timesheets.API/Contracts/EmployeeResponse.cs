using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class EmployeeResponse
    {
        public string FirstName { get; set;  }

        public string LastName { get; set; }

        public Position Position { get; set; }
    }
}