using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Domain.Telegram;

namespace Timesheets.BusinessLogic
{
    public class TelegramUsersService : ITelegramUsersService
    {
        private readonly ITelegramUsersRepository _telegramUserRepository;

        public TelegramUsersService(ITelegramUsersRepository telegramUserRepository)
        {
            _telegramUserRepository = telegramUserRepository;
        }

        public async Task<int> Create(TelegramUser telegramUser)
        {
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