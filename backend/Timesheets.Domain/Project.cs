using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public record Project
    {
        private List<WorkTime> _workTimes;

        public const int MAX_TITLE_LENGHT = 1000;

        private Project(int id, string title, WorkTime[] workTimes)
        {
            Id = id;
            Title = title;
            WorkTimes = workTimes;
        }

        public int Id { get; }

        public string Title { get; }

        public WorkTime[] WorkTimes
        {
            get
            {
                return _workTimes.ToArray();
            }

            private set
            {
                _workTimes = value.ToList();
            }
        }

        public static Result<Project> Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<Project>("Title cannot be null or empty.");
            }

            if (title.Length > MAX_TITLE_LENGHT)
            {
                return Result.Failure<Project>($"Title cannot contains more then {MAX_TITLE_LENGHT} symbols.");
            }

            return new Project(default, title, Array.Empty<WorkTime>());
        }
    }
}