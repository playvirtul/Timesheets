using System.Security.Cryptography;
using System.Text;

namespace Timesheets.Domain.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}