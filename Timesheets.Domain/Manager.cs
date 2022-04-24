namespace Timesheets.Domain
{
    public record Manager : Employee
    {
        public Manager(int id, string firstName, string lastName)
            : base(id, firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }
    }
}