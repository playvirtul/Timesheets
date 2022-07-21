namespace Timesheets.Domain
{
    public record StaffEmployee : Employee
    {
        private StaffEmployee(int id, string firstName, string lastName)
            : base(id, firstName, lastName, Position.StaffEmployee, Array.Empty<Project>())
        {
        }

        public static (StaffEmployee? Result, string[] Errors) Create(int userId, string firstName, string lastName)
        {
            return (new StaffEmployee(userId, firstName, lastName), ValidationErrors(firstName, lastName));
        }
    }
}