namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Salary
    {
        public int Position { get; set; }

        public int MonthSalary { get; set; }

        public int MonthBonus { get; set; }

        public int SalaryPerHour { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}