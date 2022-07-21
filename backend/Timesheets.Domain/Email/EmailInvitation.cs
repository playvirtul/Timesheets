namespace Timesheets.Domain
{
    public record EmailInvitation : Invitation
    {
        private EmailInvitation(string email, string firstName, string lastName, Position position)
            : base(firstName, lastName, position)
        {
            Email = email;
        }

        public string Email { get; }
    }
}