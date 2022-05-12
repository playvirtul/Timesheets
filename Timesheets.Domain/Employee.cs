namespace Timesheets.Domain
{
    public abstract record Employee
    {
        public const int MAX_FIRSTNAME_LENGTH = 100;

        public const int MAX_LASTNAME_LENGTH = 100;

        protected Employee(int id, string firstName, string lastName, Position position)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Position Position { get; }
    }
}