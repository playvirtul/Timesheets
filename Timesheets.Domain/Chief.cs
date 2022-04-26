namespace Timesheets.Domain
{
    public record Chief : Employee
    {
        private Chief(int id, string firstName, string lastName)
            : base(id, firstName, lastName)
        {
        }

        public (Employee? Result, string[] Errors) Create(string firstName, string lastName, Position position)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_FIRSTNAME_LENGTH)
            {
                return (null, new string[] { "FirstName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LASTNAME_LENGTH)
            {
                return (null, new string[] { "LastName cannot be null or empty or greater then 100 symbols." });
            }

            switch (position)
            {
                case Position.Chief:
                    return Chief.Create(firstName, lastName);

                case Position.StuffEmployee:
                    return StuffEmployee.Create(firstName, lastName);

                case Position.Manager:
                    return Manager.Create(firstName, lastName);

                case Position.Freelancer:
                    return Freelancer.Create(firstName, lastName);

                default:
                    return (null, new string[] { "Position is incorrect" });
            }
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }

        public static (Chief? Result, string[] Errors) Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_FIRSTNAME_LENGTH)
            {
                return (null, new string[] { "FirstName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LASTNAME_LENGTH)
            {
                return (null, new string[] { "LastName cannot be null or empty or greater then 100 symbols." });
            }

            return (new Chief(0, firstName, lastName), Array.Empty<string>());
        }
    }
}