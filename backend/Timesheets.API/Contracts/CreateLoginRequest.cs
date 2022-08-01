using System.ComponentModel.DataAnnotations;

namespace Timesheets.API.Contracts
{
    public class CreateLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}