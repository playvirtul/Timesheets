namespace Timesheets.Domain
{
    public record Project
    {
        private readonly List<int> _workingHours;

        private Project(int id, string? title, int[] workingHours)
        {
            Id = id;
            Title = title;
            _workingHours = new List<int>(workingHours);
        }

        public int Id { get; init; }

        public string? Title { get; init; }

        public int[] WorkingHours => _workingHours.ToArray();


        public static (Project? Result, string[] Errors) Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return (null, new string[] { "Title cannot be null or empty." });
            }

            if (title.Length > 200)
            {
                return (null, new string[] { "Title cannot contains more then 200 symbols." });
            }

            return (
                new Project(0, title, Array.Empty<int>()),
                Array.Empty<string>());
        }

        public string[] AddHours(int hours)
        {
            if (hours <= 0 || hours > 24)
            {
                return new string[] { "Hours should be between 0 and 24" };
            }

            _workingHours.Add(hours);

            return Array.Empty<string>();
        }
    }
}