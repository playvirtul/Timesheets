using Telegram.Bot;
using Timesheets.Domain;
using Timesheets.Domain.Interfaces;

namespace Timesheets.TelegramApiClient
{
    public class TelegramApiClient : ITelegramApiClient
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramApiClient(string token)
        {
            _botClient = new TelegramBotClient(token);
        }

        public async Task<bool> SendTelegramInvite(TelegramInvitation invitaion, long chatId)
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Здравствуйте, {invitaion.FirstName} {invitaion.LastName}!\n Код для регистрации - {invitaion.Code}");

            return true;
        }

        public async Task<bool> SendTelegramReport(long chatId, string message)
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message);

            return true;
        }
    }
}