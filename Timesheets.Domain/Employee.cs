namespace Timesheets.Domain
{
    public abstract record Employee
    {
        public const int MAX_FIRSTNAME_LENGTH = 100;

        public const int MAX_LASTNAME_LENGTH = 100;

        protected Employee(
            int id,
            string firstName,
            string lastName,
            Position position,
            List<Project> projects)
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

        public List<Project> Projects { get; }

        public string AddWorkTime(int projectId, WorkTime workTime)
        {
            if (Projects.Any(p => p.Id == projectId) == false)
            {
                return new string("Employee is not part of the project");
            }

            var workTimes = Projects
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
    }
}