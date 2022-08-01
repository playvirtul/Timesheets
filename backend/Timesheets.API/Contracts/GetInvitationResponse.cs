using System.ComponentModel.DataAnnotations;
using Timesheets.Domain;
using Timesheets.Domain.Auth;

namespace Timesheets.API.Contracts
{

    public class GetInvitationResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Position Position { get; set; }
    }
}