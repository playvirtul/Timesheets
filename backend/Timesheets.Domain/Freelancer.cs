namespace Timesheets.Domain
{
    public record Freelancer : Employee
    {
        private Freelancer(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Freelancer, Array.Empty<Project>())
        {
        }

        public static Freelancer Create(int userId, string firstName, string lastName)
        {
            return new Freelancer(userId, firstName, lastName);
        }
    }
}