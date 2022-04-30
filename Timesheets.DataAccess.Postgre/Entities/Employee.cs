using System.Collections.Generic;
using Timesheets.Domain;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        
        public Position Position { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<Project> Projects { get; set; } = new();
    }
}