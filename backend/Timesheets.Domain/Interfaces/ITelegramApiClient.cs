using Timesheets.Domain.Telegram;

namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramApiClient
    {
        Task<bool> SendTelegramInvite(TelegramInvitation invitaion, long chatId);

        Task<bool> SendTelegramReport(long chatId, string message);
    }
}