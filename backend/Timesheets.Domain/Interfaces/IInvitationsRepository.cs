namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationsRepository
    {
        Task<Invitation?> Get(string code);

        Task Add(Invitation newInvitation);

        Task Delete(string code);
    }
}