using System.ComponentModel.DataAnnotations;

namespace Timesheets.API.Contracts
{
    public class NewProject
    {
        [Required]
        [StringLength(1000)]
        public string Title { get; set; }
    }
}