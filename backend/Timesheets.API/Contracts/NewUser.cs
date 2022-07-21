using System.ComponentModel.DataAnnotations;
using Timesheets.Domain.Auth;

namespace Timesheets.API.Contracts
{
    public class NewUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}