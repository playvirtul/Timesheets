namespace Timesheets.Domain
{
    public record Salary
    {
        private Salary(int employeeId, decimal amount, decimal bonus, SalaryType salaryType)
        {
            EmployeeId = employeeId;
            Amount = amount;
            Bonus = bonus;
            SalaryType = salaryType;
        }

        public int EmployeeId { get; }

        public SalaryType SalaryType { get; }

        public decimal Amount { get; }

        public decimal Bonus { get; }

        public static (Salary? Result, string[] Errors) Create(
            int employeeId,
            decimal amount,
            decimal bonus,
            SalaryType salaryType)
        {
            if (amount <= 0)
            {
                return (null, new string[] { "Amount cannot be less than zero" });
            }

            return (new Salary(employeeId, amount, bonus, salaryType), Array.Empty<string>());
        }

        public decimal CalculateSalaryAmount(WorkTime[] workTimes)
        {
            decimal salaryAmount = 0;

            switch (SalaryType)
            {
                case SalaryType.Hourly:
                    salaryAmount = Amount * workTimes.Sum(w => w.Hours);
                    return salaryAmount;

                case SalaryType.Monthly:
                    var workTimesGroupsByDay = workTimes
                        .GroupBy(w => w.Date.Day)
                        .ToArray();

                    foreach (var workTimesPerDay in workTimesGroupsByDay)
                    {
                        var hoursByDay = workTimesPerDay.Sum(w => w.Hours);

                        if (hoursByDay > WorkTime.MAX_WORKING_HOURS_PER_DAY)
                        {
                            var overTimeHours = hoursByDay - WorkTime.MAX_WORKING_HOURS_PER_DAY;

                            salaryAmount += FormulaCalculation(overTimeHours, Bonus);

                            salaryAmount += FormulaCalculation(WorkTime.MAX_WORKING_HOURS_PER_DAY, Amount);
                        }
                        else
                        {
                            salaryAmount += FormulaCalculation(hoursByDay, Amount);
                        }
                    }

                    return salaryAmount;

                default:
                    return default;
            }
        }

        private static decimal FormulaCalculation(int workingHours, decimal amount)
        {
            return workingHours / (decimal)WorkTime.MAX_WORKING_HOURS_PER_MONTH * amount;
        }
    }
}