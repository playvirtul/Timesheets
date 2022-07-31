using System;
using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class WorkTimeRequest
    {
        [Required]
        [Range(WorkTime.MIN_WORKING_HOURS_PER_DAY, WorkTime.MAX_OVERTIME_HOURS_PER_DAY)]
        public int Hours { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}