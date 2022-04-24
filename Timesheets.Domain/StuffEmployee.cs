namespace Timesheets.Domain
{
    public record StuffEmployee : Employee
    {
        public StuffEmployee(int id, string firstName, string lastName)
            : base(id, firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }
    }
}