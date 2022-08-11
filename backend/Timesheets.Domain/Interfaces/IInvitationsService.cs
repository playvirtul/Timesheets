using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface IInvitationsService
    {
        Task<Result> Create(TelegramInvitation invitation);

        Task<Result<TelegramInvitation>> Get(string code);
    }
}