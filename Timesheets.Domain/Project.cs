namespace Timesheets.Domain
{
    public record Project
    {
        //private List<WorkTime> _workTimes;

        public const int MAX_TITLE_LENGHT = 1000;

        public Project(int id, string title, List<WorkTime> workTimes)
        {
            Id = id;
            Title = title;
            WorkTimes = workTimes;
        }

        public int Id { get; }

        public string Title { get; }

        public IReadOnlyList<WorkTime> WorkTimes { get; }

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

            return (new Project(default, title, new List<WorkTime>()),
                    Array.Empty<string>());
        }
    }
}