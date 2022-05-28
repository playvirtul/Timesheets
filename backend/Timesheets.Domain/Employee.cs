namespace Timesheets.Domain
{
    public abstract record Employee
    {
        private List<Project> _projects;

        public const int MAX_FIRSTNAME_LENGTH = 100;

        public const int MAX_LASTNAME_LENGTH = 100;

        protected Employee(
            int id,
            string firstName,
            string lastName,
            Position position,
            Project[] projects)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Projects = projects;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Position Position { get; }

        public Project[] Projects
        {
            get
            {
                return _projects.ToArray();
            }

            private set
            {
                _projects = value.ToList();
            }
        }

        public string AddWorkTime(int projectId, WorkTime workTime)
        {
            if (_projects.Any(p => p.Id == projectId) == false)
            {
                return new string("Employee is not part of the project");
            }

            var workTimes = _projects
                .Select(p => p.WorkTimes.Where(w => w.EmployeeId == Id));

            var hoursPerDay = workTimes
                .Sum(w => w.Where(w => w.Date.ToShortDateString() == workTime.Date.ToShortDateString())
                    .Sum(w => w.Hours));

            if (workTime.Hours + hoursPerDay > WorkTime.MAX_OVERTIME_HOURS_PER_DAY)
            {
                return new string("Can not add more than 24 hours on the same date.");
            }

            return string.Empty;
        }

        public string AddProject(int projectId)
        {
            if (Projects.Any(p => p.Id == projectId))
            {
                return new string("Employee already has a project.");
            }

            return string.Empty;
        }
    }
}