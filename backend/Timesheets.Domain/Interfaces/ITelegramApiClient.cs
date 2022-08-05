namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramApiClient
    {
        Task<bool> SendTelegramInvite(TelegramInvitation invitaion);
    }
}