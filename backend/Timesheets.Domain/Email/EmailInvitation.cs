using Timesheets.Domain.Auth;

namespace Timesheets.Domain
{
    public record EmailInvitation : Invitation
    {
        private EmailInvitation(string email, string firstName, string lastName, Position position, Role role)
            : base(firstName, lastName, position, role)
        {
            Email = email;
        }

        public string Email { get; }
    }
}