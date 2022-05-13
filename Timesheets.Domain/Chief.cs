namespace Timesheets.Domain
{
    public record Chief : Employee
    {
        private Chief(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Chief, new List<Project>())
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

            return position switch
            {
                Position.Chief => Chief.Create(firstName, lastName),
                Position.StaffEmployee => StaffEmployee.Create(firstName, lastName),
                Position.Manager => Manager.Create(firstName, lastName),
                Position.Freelancer => Freelancer.Create(firstName, lastName),
                _ => (null, new string[] { "Position is incorrect" })
            };
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