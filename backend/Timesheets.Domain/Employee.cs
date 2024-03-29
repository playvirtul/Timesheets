﻿using CSharpFunctionalExtensions;

namespace Timesheets.Domain
{
    public abstract record Employee
    {
        private List<Project> _projects = new();

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

        public static Result<Employee> Create(int userId, string firstName, string lastName, Position position)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_FIRSTNAME_LENGTH)
            {
                return Result
                    .Failure<Employee>($"FirstName cannot be null or empty or greater then {MAX_FIRSTNAME_LENGTH} symbols.");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LASTNAME_LENGTH)
            {
                return Result
                    .Failure<Employee>($"LastName cannot be null or empty or greater then {MAX_LASTNAME_LENGTH} symbols.");
            }

            return position switch
            {
                Position.Chief => Chief.Create(userId, firstName, lastName).Map<Chief, Employee>(c => c),
                Position.StaffEmployee => StaffEmployee.Create(userId, firstName, lastName).Map<StaffEmployee, Employee>(c => c),
                Position.Manager => Manager.Create(userId, firstName, lastName).Map<Manager, Employee>(c => c),
                Position.Freelancer => Freelancer.Create(userId, firstName, lastName).Map<Freelancer, Employee>(c => c),
                _ => Result.Failure<Employee>("Position is incorrect")
            };
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
                return new string($"Can not add more than {WorkTime.MAX_OVERTIME_HOURS_PER_DAY} hours on the same date.");
            }

            return string.Empty;
        }

        public string AddProject(int projectId)
        {
            if (_projects.Any(p => p.Id == projectId))
            {
                return new string("Employee already has a project.");
            }

            return string.Empty;
        }

        protected static Result ValidationErrors(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_FIRSTNAME_LENGTH)
            {
                return Result.Failure($"FirstName cannot be null or empty or greater then {MAX_FIRSTNAME_LENGTH} symbols.");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LASTNAME_LENGTH)
            {
                return Result.Failure($"LastName cannot be null or empty or greater then {MAX_LASTNAME_LENGTH} symbols.");
            }

            return Result.Success();
        }
    }
}