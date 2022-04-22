using System.Collections.Generic;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<WorkTime> WorkTimes { get; set; } = new();

        public List<Employee> Employees { get; set; } = new();
    }
}