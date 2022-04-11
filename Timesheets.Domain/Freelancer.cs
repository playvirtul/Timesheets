namespace Timesheets.Domain
{
    public class Freelancer : Employee
    {
        public Freelancer(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }
    }
}