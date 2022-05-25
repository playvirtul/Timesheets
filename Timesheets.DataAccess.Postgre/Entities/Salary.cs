using Timesheets.Domain;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Salary
    {
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public decimal Amount { get; set; }

        public decimal Bonus { get; set; }

        public SalaryType SalaryType { get; set; }
    }
}