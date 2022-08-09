namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationsRepository
    {
        Task<TelegramInvitation?> Get(string code);

        Task Add(TelegramInvitation newInvitation);

        Task Accept(string code);
    }
}