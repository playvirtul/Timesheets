using CSharpFunctionalExtensions;
using Timesheets.Domain.Auth;

namespace Timesheets.Domain
{
    public record TelegramInvitation : Invitation
    {
        public const int MIN_USERNAME_LENGTH = 5;
        public const int MAX_USERNAME_LENGTH = 32;

        private TelegramInvitation(string userName, string firstName, string lastName, Position position, Role role)
            : base(firstName, lastName, position, role)
        {
            UserName = userName;
        }

        public string UserName { get; }

        public static Result<TelegramInvitation> Create(
            string userName,
            string firstName,
            string lastName,
            Position position,
            Role role)
        {
            if (string.IsNullOrWhiteSpace(userName) || (userName.Length > MAX_USERNAME_LENGTH && userName.Length < MIN_USERNAME_LENGTH))
            {
                return Result
                    .Failure<TelegramInvitation>($"UserName cannot be null, empty, greater then {MAX_USERNAME_LENGTH} symbols or" +
                        $"less then {MIN_USERNAME_LENGTH} symbols.");
            }

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Employee.MAX_LASTNAME_LENGTH)
            {
                return Result
                   .Failure<TelegramInvitation>($"FirstName cannot be null or empty or greater then " +
                        $"{Employee.MAX_FIRSTNAME_LENGTH} symbols.");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Employee.MAX_LASTNAME_LENGTH)
            {
                return Result
                    .Failure<TelegramInvitation>($"LastName cannot be null or empty or greater then " +
                        $"{Employee.MAX_LASTNAME_LENGTH} symbols.");
            }

            return new TelegramInvitation(userName, firstName, lastName, position, role);
        }
    }
}