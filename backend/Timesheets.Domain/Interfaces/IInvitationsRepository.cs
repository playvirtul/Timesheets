namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationsRepository
    {
        Task<TelegramInvitation?> Get(string code);

        Task Add(TelegramInvitation newInvitation);

        Task Delete(string code);
    }
}