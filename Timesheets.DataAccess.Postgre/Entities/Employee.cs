using System;
using System.Collections.Generic;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Project> Projects { get; set; } = Array.Empty<Project>();

        //public Salary Salary { get; set; }
    }
}