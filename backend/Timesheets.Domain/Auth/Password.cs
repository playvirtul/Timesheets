using System.Security.Cryptography;
using System.Text;

namespace Timesheets.Domain.Auth
{
    public record Password
    {
        public Password(string password)
        {
            Value = password;
        }

        public string Value { get; }

        public string Hash()
        {
            using var sha256 = SHA256.Create();

            var passwordBytes = Encoding.UTF8.GetBytes(Value);
            var hashBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}