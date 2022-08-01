using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class CreateSalaryRequest
    {
        [Required]
        public SalaryType SalaryType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Bonus { get; set; }
    }
}