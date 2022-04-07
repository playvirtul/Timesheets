namespace Timesheets.Domain
{
    public class Project
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string EmployeeName { get; set; }

        public int WorkingHours { get; set; } = 0;
    }
}