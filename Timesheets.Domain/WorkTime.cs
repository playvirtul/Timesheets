﻿namespace Timesheets.Domain
{
    public class WorkTime
    {
        public WorkTime(int projectId, int hours, DateTime date)
        {
            ProjectId = projectId;
            WorkingHours = hours;
            Date = date;
        }

        public int ProjectId { get; set; }

        public int WorkingHours { get; set; }

        public DateTime Date { get; set; }

        public static (WorkTime? Result, string[] Errors) Create(int projectId, int hours, DateTime date)
        {
            if (projectId <= default(int))
            {
                return (null, new string[] { "Id cannot be less then 1." });
            }

            if (hours <= 0 || hours > 24)
            {
                return (null, new string[] { "Hours should be between 0 and 24." });
            }

            if (date > DateTime.Now)
            {
                return (null, new string[] { "You cannot select a future date." });
            }

            return (
                new WorkTime(projectId, hours, date),
                Array.Empty<string>());
        }
    }
}