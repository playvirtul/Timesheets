using System.Collections.Generic;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Salary Salary { get; set; } = null!;

        public List<Project> Projects { get; set; } = new();
    }
}