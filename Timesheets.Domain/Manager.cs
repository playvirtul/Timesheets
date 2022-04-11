namespace Timesheets.Domain
{
    public class Manager : Employee
    {
        public Manager(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }
    }
}