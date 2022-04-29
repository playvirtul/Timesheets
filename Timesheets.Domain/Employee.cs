namespace Timesheets.Domain
{
    public abstract record Employee
    {
        public const int MAX_FIRSTNAME_LENGTH = 100;

        public const int MAX_LASTNAME_LENGTH = 100;

        protected Employee(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        //public List<Project> Projects { get; }

        public Salary Salary { get; set; } = null!;

        public void SetupSalary(Salary salary)
        {
            Salary = salary with
            {
                Position = salary.Position,
                MonthSalary = salary.MonthSalary,
                MonthBonus = salary.MonthBonus,
                SalaryPerHour = salary.SalaryPerHour
            };
        }

        public abstract decimal CalculateSalary(Project project);
    }
}