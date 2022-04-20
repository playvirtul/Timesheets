using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class NewEmployee
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public Position Position { get; set; }
    }
}