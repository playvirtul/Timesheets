using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public record WorkTime
    {
        public const int MIN_WORKING_HOURS_PER_DAY = 1;
        public const int MAX_WORKING_HOURS_PER_DAY = 8;
        public const int MAX_OVERTIME_HOURS_PER_DAY = 24;
        public const int MAX_WORKING_HOURS_PER_MONTH = 160;

        private WorkTime(int employeeId, int projectId, int hours, DateTime date)
        {
            EmployeeId = employeeId;
            ProjectId = projectId;
            Hours = hours;
            Date = date;
        }

        public int EmployeeId { get; }

        public int ProjectId { get; }

        public int Hours { get; }

        public DateTime Date { get;  }

        public static Result<WorkTime> Create(int employeeId, int projectId, int hours, DateTime date)
        {
            if (hours < MIN_WORKING_HOURS_PER_DAY || hours > MAX_OVERTIME_HOURS_PER_DAY)
            {
                return Result.Failure<WorkTime>("Hours should be between 0 and 24.");
            }

            if (date > DateTime.Now)
            {
                return Result.Failure<WorkTime>("You cannot select a future date.");
            }

            return new WorkTime(employeeId, projectId, hours, date);
        }
    }
}