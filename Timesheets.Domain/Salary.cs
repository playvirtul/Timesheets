namespace Timesheets.Domain
{
    public record Salary
    {
        private Salary(int employeeId, decimal amount, SalaryType salaryType)
        {
            EmployeeId = employeeId;
            Amount = amount;
            SalaryType = salaryType;
        }

        public int EmployeeId { get; }

        public SalaryType SalaryType { get; }

        public decimal Amount { get; }

        public decimal CalculateSalaryAmount(int employeeId)
        {
            return 0;
        }

        public static (Salary? Result, string[] Errors) Create(int employeeId, decimal amount, SalaryType salaryType)
        {
            if (amount <= 0)
            {
                return (null, new string[] { "Amount cannot be less than zero" });
            }

            return (new Salary(employeeId, amount, salaryType), Array.Empty<string>());
        }
    }
}