namespace Timesheets.Domain
{
    public record Project
    {
        public const int MAX_TITLE_LENGHT = 1000;

        public Project(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; }

        public string Title { get; }

        public static string CreateWorkTime(WorkTime workTime, WorkTime[] workTimes)
        {
            var hourPerDay = workTimes
                .Where(w => w.Date.ToShortDateString() == workTime.Date.ToShortDateString())
                .Sum(w => w.Hours);

            if (hourPerDay + workTime.Hours > WorkTime.MAX_OVERTIME_HOURS_PER_DAY)
            {
                return new string("Can not add more than 24 hours on the same date.");
            }

            return string.Empty;
        }

        public static (Project? Result, string[] Errors) Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return (null, new string[] { "Title cannot be null or empty." });
            }

            if (title.Length > MAX_TITLE_LENGHT)
            {
                return (null, new string[] { "Title cannot contains more then 1000 symbols." });
            }

            return (new Project(default, title),
                    Array.Empty<string>());
        }
    }
}