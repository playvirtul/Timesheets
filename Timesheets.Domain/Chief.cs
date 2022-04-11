namespace Timesheets.Domain
{
    public class Chief : Employee
    {
        public Chief(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        public override decimal CalculateSalary(Project project)
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(string employeeRole)
        {
            switch (employeeRole.ToLower())
            {
                case "stuffemployee":

                    break;
            }
        }
    }
}