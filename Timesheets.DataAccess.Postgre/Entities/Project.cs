using System;
using System.Collections.Generic;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<WorkTime> WorkTimes { get; set; } = Array.Empty<WorkTime>();

        public ICollection<Employee> Employees { get; set; } = Array.Empty<Employee>();
    }
}