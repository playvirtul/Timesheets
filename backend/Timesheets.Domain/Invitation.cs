namespace Timesheets.Domain
{
    public record Invitation
    {
        public string Code { get; set; }
    }

    public record EmailInvitation : Invitation
    {
        public string Email { get; set; }
    }

    public record TelegramInvitation : Invitation
    {
        public string UserName { get; set; }
    }
}