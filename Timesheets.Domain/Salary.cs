namespace Timesheets.Domain
{
    public record Salary
    {
        private Salary(int employeeId, SalaryType salaryType)
        {
            EmployeeId = employeeId;
            SalaryType = salaryType;
        }

        public int EmployeeId { get; }

        public SalaryType SalaryType { get; }

        public decimal Amount { get; }

        public decimal CalculateSalaryAmount(int employeeId)
        {
            return 0;
        }

        public static Salary Create(int employeeId, SalaryType salaryType)
        {
            return new Salary(employeeId, salaryType);
        }
    }
}