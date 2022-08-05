using Timesheets.Domain.Telegram;

namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramUsersService
    {
        Task<int> Create(TelegramUser telegramUser);
    }
}