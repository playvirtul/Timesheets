namespace Timesheets.Domain
{
    public record Manager : Employee
    {
        private Manager(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Manager, Array.Empty<Project>())
        {
        }

        public static Manager Create(int userId, string firstName, string lastName)
        {
            return new Manager(userId, firstName, lastName);
        }
    }
}