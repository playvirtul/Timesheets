using Timesheets.Domain.Telegram;

namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramUserRepository
    {
        Task<int> Create(TelegramUser telegramUser);
    }
}