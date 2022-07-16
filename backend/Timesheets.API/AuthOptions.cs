using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Timesheets.API
{
    public static class AuthOptions
    {
        //public const string ISSUER = "authServer";

        //public const string AUDIENCE = "resourceServer";

        public const string KEY = "GaLVW99Cyp5FH65e";

        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}