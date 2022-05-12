using System;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class WorkTime
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        public int Hours { get; set; }

        public DateTime Date { get; set; }
    }
}