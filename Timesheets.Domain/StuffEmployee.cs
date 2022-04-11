namespace Timesheets.Domain
{
    public class StuffEmployee : Employee
    {
        public StuffEmployee(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }
    }
}