namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationRepository
    {
        Task<Invitation?> Get(string code);

        Task Add(Invitation newInvitation);

        Task Delete(string code);
    }
}