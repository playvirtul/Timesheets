namespace Timesheets.DataAccess.Postgre.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string HashPassword { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }
    }
}