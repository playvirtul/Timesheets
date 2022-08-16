using CSharpFunctionalExtensions;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Timesheets.Domain.Auth;

namespace Timesheets.Domain
{
    public record User
    {
        private User(int id, string email, string telegramUserName, string passwordHash, Role role)
        {
            Id = id;
            Email = email;
            TelegramUserName = telegramUserName;
            PasswordHash = passwordHash;
            Role = role;
        }

        public int Id { get; }

        public string Email { get; }

        public string TelegramUserName { get; }

        public string PasswordHash { get; }

        public Role Role { get; }

        public static Result<User> Create(string email, string telegramUserName, string password, Role role)
        {
            if (IsValidEmail(email) == false)
            {
                return Result.Failure<User>("Email is incorrect");
            }

            if (IsValidPassword(password, out string errorMessage) == false)
            {
                return Result.Failure<User>(errorMessage);
            }

            var passwordHash = new Password(password).Hash();

            return new User(default, email, telegramUserName, passwordHash, role);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidPassword(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password should not be empty");
            }

            if (password.Length < 8)
            {
                errorMessage = "Password should not be lesser than 8 characters.";
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(password))
            {
                errorMessage = "Password should contain at least one lower case letter.";
                return false;
            }

            if (!hasUpperChar.IsMatch(password))
            {
                errorMessage = "Password should contain at least one upper case letter.";
                return false;
            }

            if (!hasNumber.IsMatch(password))
            {
                errorMessage = "Password should contain at least one numeric value.";
                return false;
            }

            return true;
        }
    }
}