namespace Timesheets.Domain
{
    public class Project
    {
        public const int MAX_TITLE_LENGHT = 1000;

        public Project(int id, string title, List<WorkTime> workTimes)
        {
            Id = id;
            Title = title;
            WorkTimes = workTimes;
        }

        public int Id { get; }

        public string Title { get; }

        public List<WorkTime> WorkTimes { get; }

        //public WorkTime[] WorkingHours => _workingHours.ToArray();

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

            return (new Project(0, title, new List<WorkTime>()),
                    Array.Empty<string>());
        }

        public string[] AddWorkTime(WorkTime workTime)
        {
            var hoursPerDay = WorkTimes
                .Where(x => x.Date.Day == workTime.Date.Day)
                .Sum(x => x.Hours);

            if (workTime.Hours + hoursPerDay > 24)
            {
                return new string[] { "can not add more than 24 hours on the same date." };
            }

            return Array.Empty<string>();
        }
    }
}