using CSharpFunctionalExtensions;
using Timesheets.Domain.Auth;

namespace Timesheets.Domain
{
    public class User
    {
        public int Id { get; }

        public string Email { get; }

        public string PasswordHash { get; }

        public Role Role { get; }

        private User(int id, string email, string passwordHash, Role role)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public static Result<User> Create(string email, string password, Role role)
        {
            //валидация пароля и email
            var passwordHash = new Password(password).Hash();

            return new User(default, email, passwordHash, role);
        }
    }
}