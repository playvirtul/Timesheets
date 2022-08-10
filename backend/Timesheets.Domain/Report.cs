namespace Timesheets.Domain
{
    public record Report
    {
        public int Hours { get; init; }

        public decimal SalaryAmount { get; init; }
    }
}