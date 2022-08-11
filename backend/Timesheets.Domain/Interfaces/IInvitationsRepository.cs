using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationsRepository
    {
        Task<TelegramInvitation?> Get(string code);

        Task<Result> Add(Domain.TelegramInvitation newInvitation);

        Task Accept(string code);
    }
}