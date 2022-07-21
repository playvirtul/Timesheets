namespace Timesheets.Domain.Telegram
{
    public interface ITelegramApiClient
    {
        Task<bool> SendTelegramInvite(TelegramInvitation invitaion);
    }
}