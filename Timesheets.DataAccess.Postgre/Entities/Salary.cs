using Timesheets.Domain;

namespace Timesheets.DataAccess.Postgre.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        
        public Employee Employee { get; set; }
        
        public int EmployeeId { get; set; }
        
        public decimal Amount { get; set; }
        
        public SalaryType SalaryType { get; set; }
    }
}