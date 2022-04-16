namespace Timesheets.Domain
{
    public abstract class Employee
    {
        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static (Employee? Result, string[] Errors) Create(string firstName, string lastName, EmployeeRole position)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 100)
            {
                return (null, new string[] { "FirstName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 100)
            {
                return (null, new string[] { "LastName cannot be null or empty or greater then 100 symbols." });
            }

            switch (position)
            {
                case EmployeeRole.Chief:
                    return (new Chief(firstName, lastName), Array.Empty<string>());

                case EmployeeRole.StuffEmployee:
                    return (new StuffEmployee(firstName, lastName), Array.Empty<string>());

                case EmployeeRole.Manager:
                    return (new Manager(firstName, lastName), Array.Empty<string>());

                case EmployeeRole.Freelancer:
                    return (new Freelancer(firstName, lastName), Array.Empty<string>());

                default:
                    return (null, new string[] { "Position is incorrect" });
            }
        }

        public abstract decimal CalculateSalary(Project project);
    }
}