using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class NewSalary
    {
        [Required]
        public SalaryType SalaryType { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}