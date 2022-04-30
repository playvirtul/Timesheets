namespace Timesheets.Domain
{
    public record Salary
    {
        public int EmployeeId { get; set; }
        
        public decimal Amount { get; set; }
        
        public SalaryType SalaryType { get; set; }
    }
}