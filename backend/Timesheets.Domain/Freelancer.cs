namespace Timesheets.Domain
{
    public record Freelancer : Employee
    {
        public Freelancer(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Freelancer, Array.Empty<Project>())
        {
        }

        public static (Freelancer? Result, string[] Errors) Create(int userId, string firstName, string lastName)
        {
            return (new Freelancer(userId, firstName, lastName), ValidationErrors(firstName, lastName));
        }
    }
}