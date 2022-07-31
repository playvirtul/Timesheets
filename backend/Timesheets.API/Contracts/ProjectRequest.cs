using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class ProjectRequest
    {
        [Required]
        [StringLength(Project.MAX_TITLE_LENGHT)]
        public string Title { get; set; }
    }
}