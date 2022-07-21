namespace Timesheets.Domain
{
    public record Manager : Employee
    {
        public Manager(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.Manager, Array.Empty<Project>())
        {
        }

        public static (Manager? Result, string[] Errors) Create(int userId, string firstName, string lastName)
        {
            return (new Manager(userId, firstName, lastName), ValidationErrors(firstName, lastName));
        }
    }
}