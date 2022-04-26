namespace Timesheets.Domain
{
    public record WorkTime
    {
        private const int MAX_WORKING_HOURS_PER_DAY = 24;

        private WorkTime(int projectId, int hours, DateTime date)
        {
            ProjectId = projectId;
            Hours = hours;
            Date = date;
        }

        public int ProjectId { get; init; }

        public int Hours { get; init; }

        public DateTime Date { get; init; }

        public static (WorkTime? Result, string[] Errors) Create(int projectId, int hours, DateTime date)
        {
            if (projectId <= default(int))
            {
                return (null, new string[] { "Id cannot be less then 1." });
            }

            if (hours <= 0 || hours > MAX_WORKING_HOURS_PER_DAY)
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