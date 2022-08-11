using CSharpFunctionalExtensions;
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

        public async Task<Result> SendTelegramInvite(TelegramInvitation invitaion, long chatId)
        {
            try
            {
                await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Здравствуйте, {invitaion.FirstName} {invitaion.LastName}!\n Код для регистрации - {invitaion.Code}");
            }
            catch
            {
                return Result.Failure("Failed to send invitation");
            }

            return Result.Success();
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