namespace Timesheets.Domain
{
    public record Freelancer : Employee
    {
        public Freelancer(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Freelancer, Array.Empty<Project>())
        {
        }

        public static (Freelancer? Result, string[] Errors) Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_FIRSTNAME_LENGTH)
            {
                return (null, new string[] { "FirstName cannot be null or empty or greater then 100 symbols." });
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LASTNAME_LENGTH)
            {
                return (null, new string[] { "LastName cannot be null or empty or greater then 100 symbols." });
            }

            return (new Freelancer(0, firstName, lastName), Array.Empty<string>());
        }
    }
}