namespace Timesheets.Domain
{
    public abstract class Employee
    {
        private const int MAX_STRING_LENGTH = 100;

        protected Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public static (Employee? Result, string[] Errors) Create(string firstName, string lastName, Position position)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_STRING_LENGTH)
            {
                return (null, new string[] { "FirstName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_STRING_LENGTH)
            {
                return (null, new string[] { "LastName cannot be null or empty or greater then 100 symbols." });
            }

            switch (position)
            {
                case Position.Chief:
                    return (new Chief(firstName, lastName), Array.Empty<string>());

                case Position.StuffEmployee:
                    return (new StuffEmployee(firstName, lastName), Array.Empty<string>());

                case Position.Manager:
                    return (new Manager(firstName, lastName), Array.Empty<string>());

                case Position.Freelancer:
                    return (new Freelancer(firstName, lastName), Array.Empty<string>());

                default:
                    return (null, new string[] { "Position is incorrect" });
            }
        }

        public abstract decimal CalculateSalary(Project project);
    }
}