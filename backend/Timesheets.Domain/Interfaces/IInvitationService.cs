namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationService
    {
        Task Create(Invitation invitation);

        Task<Invitation?> Get(string code);
    }
}