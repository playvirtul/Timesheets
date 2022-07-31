using Telegram.Bot;
using Timesheets.Domain;
using Timesheets.Domain.Telegram;

namespace Timesheets.TelegramApiClient
{
    public class TelegramApiClient : ITelegramApiClient
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramApiClient(string token)
        {
            _botClient = new TelegramBotClient(token);
        }

        public async Task<bool> SendTelegramInvite(TelegramInvitation invitaion)
        {
            await _botClient.SendTextMessageAsync(
                chatId: "312433636",
                text: $"Здравствуйте, {invitaion.FirstName} {invitaion.LastName}!\n Код для регистрации - {invitaion.Code}");

            return true;
        }
    }
}