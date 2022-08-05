using System.Threading.Tasks;
using Timesheets.Domain.Interfaces;
using Timesheets.Domain.Telegram;

namespace Timesheets.BusinessLogic
{
    public class TelegramUsersService : ITelegramUsersService
    {
        private readonly ITelegramUserRepository _telegramUserRepository;

        public TelegramUsersService(ITelegramUserRepository telegramUserRepository)
        {
            _telegramUserRepository = telegramUserRepository;
        }

        public async Task<int> Create(TelegramUser telegramUser)
        {
            return await _telegramUserRepository.Create(telegramUser);
        }
    }
}