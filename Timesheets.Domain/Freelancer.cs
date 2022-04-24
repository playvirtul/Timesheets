namespace Timesheets.Domain
{
    public record Freelancer : Employee
    {
        public Freelancer(int id, string firstName, string lastName)
            : base(id, firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }
    }
}