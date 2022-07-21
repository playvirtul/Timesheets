using Timesheets.Domain.Auth;

namespace Timesheets.Domain
{
    public class User
    {
        public string Email { get; }

        public string PasswordHash { get; }

        public Role Role { get; }

        private User(string email, string passwordHash, Role role)
        {
            Email = email;
            PasswordHash = new Password(passwordHash).Hash();
            Role = role;
        }

        public static (User Result, string[] Errors) Create(string email, string password, Role role)
        {
            //валидация пароля и email

            return (new User(email, password, role), Array.Empty<string>());
        }
    }
}