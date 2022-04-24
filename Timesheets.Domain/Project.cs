namespace Timesheets.Domain
{
    public record Project
    {
        //private readonly List<WorkTime> _workingHours;

        public Project(int id, string title, List<WorkTime> workTimes)
        {
            Id = id;
            Title = title;
            WorkTimes = workTimes;
        }

        public int Id { get; init; }

        public string Title { get; init; }

        public List<WorkTime> WorkTimes { get; init; }

        //public WorkTime[] WorkingHours => _workingHours.ToArray();

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

            return (new Project(0, title, new List<WorkTime>()),
                    Array.Empty<string>());
        }

        //public string[] AddWorkTime(WorkTime workTime)
        //{
        //    if (workTime.WorkingHours + CountHoursPerDay(workTime) > 24)
        //    {
        //        return new string[] { "can not add more than 24 hours on the same date." };
        //    }

        //    _workingHours.Add(workTime);

        //    return Array.Empty<string>();
        //}

        //private int CountHoursPerDay(WorkTime workTime)
        //{
        //    var hoursPerDay = _workingHours
        //        .Where(x => x.Date.Day == workTime.Date.Day)
        //        .Sum(x => x.WorkingHours);

        //    return hoursPerDay;
        //}
    }
}