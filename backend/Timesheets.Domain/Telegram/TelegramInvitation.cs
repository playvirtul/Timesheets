namespace Timesheets.Domain
{
    public record TelegramInvitation : Invitation
    {
        public const int MIN_USERNAME_LENGTH = 5;
        public const int MAX_USERNAME_LENGTH = 32;

        private TelegramInvitation(string firstName, string lastName, Position position)
            : base(firstName, lastName, position)
        {
        }

        private TelegramInvitation(string userName, string firstName, string lastName, Position position)
            : base(firstName, lastName, position)
        {
            UserName = userName;
        }

        public string UserName { get; }

        public static (TelegramInvitation? Result, string[] Errors) Create(
            string userName,
            string firstName,
            string lastName,
            Position position)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(userName) || (userName.Length > MAX_USERNAME_LENGTH && userName.Length < MIN_USERNAME_LENGTH))
            {
                errors.Add(new string($"UserName cannot be null, empty, greater then {MAX_USERNAME_LENGTH} symbols or" +
                    $"less then {MIN_USERNAME_LENGTH} symbols."));
            }

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Employee.MAX_LASTNAME_LENGTH)
            {
                errors.Add(new string($"FirstName cannot be null or empty or greater then {Employee.MAX_FIRSTNAME_LENGTH} symbols."));
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Employee.MAX_LASTNAME_LENGTH)
            {
                errors.Add(new string($"LastName cannot be null or empty or greater then {Employee.MAX_LASTNAME_LENGTH} symbols."));
            }

            return (new TelegramInvitation(userName, firstName, lastName, position), errors.ToArray());
        }
    }
}