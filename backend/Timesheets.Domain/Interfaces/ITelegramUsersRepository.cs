using Timesheets.Domain.Telegram;

namespace Timesheets.Domain.Interfaces
{
    public interface ITelegramUsersRepository
    {
        Task<int> Create(TelegramUser telegramUser);

        Task<TelegramUser?> Get(string userName);
    }
}