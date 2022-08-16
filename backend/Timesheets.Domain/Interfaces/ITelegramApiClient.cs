using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramApiClient
    {
        Task<Result> SendTelegramInvite(TelegramInvitation invitaion, long chatId);

        Task<bool> SendTelegramReport(long chatId, string message);
    }
}