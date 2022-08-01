using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;

namespace Timesheets.API.Contracts
{
    public class GetProjectResponse
    {
        public string Title { get; set; }
    }
}