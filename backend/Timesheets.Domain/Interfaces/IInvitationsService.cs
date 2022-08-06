using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationsService
    {
        Task Create(TelegramInvitation invitation);

        Task<Result<TelegramInvitation>> Get(string code);
    }
}