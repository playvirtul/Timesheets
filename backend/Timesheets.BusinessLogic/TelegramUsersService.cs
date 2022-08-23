using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Domain.Telegram;

namespace Timesheets.BusinessLogic
{
    public class TelegramUsersService : ITelegramUsersService
    {
        private readonly ITelegramUsersRepository _telegramUserRepository;
        private readonly Domain.Interfaces.ITelegramApiClient _telegramApiClient;

        public TelegramUsersService(ITelegramUsersRepository telegramUserRepository, ITelegramApiClient telegramApiClient)
        {
            _telegramUserRepository = telegramUserRepository;
            _telegramApiClient = telegramApiClient;
        }

        public async Task<int> Create(TelegramUser telegramUser)
        {
            await _telegramApiClient.SendTelegramMessage(
                telegramUser.ChatId,
                "Вы удачно зарегистрированы в системе! Ожидайте приглашения.");

            return await _telegramUserRepository.Create(telegramUser);
        }

        public async Task<Result<TelegramUser>> Get(string username)
        {
            var telegramUser = await _telegramUserRepository.Get(username);

            if (telegramUser == null)
            {
                return Result.Failure<TelegramUser>("User not found");
            }

            return telegramUser;
        }
    }
}