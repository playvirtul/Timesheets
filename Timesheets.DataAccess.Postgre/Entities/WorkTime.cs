using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class WorkTime
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        [Range(Domain.WorkTime.MIN_WORKING_HOURS_PER_DAY, Domain.WorkTime.MAX_OVERTIME_HOURS_PER_DAY)]
        public int Hours { get; set; }

        public DateTime Date { get; set; }
    }
}