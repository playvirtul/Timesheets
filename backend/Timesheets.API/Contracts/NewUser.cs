using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

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
        public Position Position { get; set; }
    }
}