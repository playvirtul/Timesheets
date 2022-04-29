namespace Timesheets.Domain
{
    public record Salary
    {
        public int Position { get; set; }

        public int MonthSalary { get; init; }

        public int MonthBonus { get; init; }

        public int SalaryPerHour { get; init; }
    }
}