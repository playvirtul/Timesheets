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

        public string Salary { get; set; }

        public static (Employee? Result, string[] Errors) Create(string firstName, string lastName, string position)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 100)
            {
                return (null, new string[] { "FirstName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 100)
            {
                return (null, new string[] { "LastName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                return (null, new string[] { "Position cannot be null or empty." });
            }

            switch (position.ToLower())
            {
                case "stuffemployee":
                    return (new StuffEmployee(firstName, lastName), Array.Empty<string>());

                case "manager":
                    return (new Manager(firstName, lastName), Array.Empty<string>());

                case "freelancer":
                    return (new Freelancer(firstName, lastName), Array.Empty<string>());

                case "chief":
                    return (new Chief(firstName, lastName), Array.Empty<string>());

                default:
                    return (null, new string[] { "Position is incorrect" });
            }
        }

        public abstract decimal CalculateSalary(Project project);
    }
}