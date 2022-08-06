using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Telegram
{
    public class TelegramUser
    {
        private TelegramUser(string userName, long chatId)
        {
            UserName = userName;
            ChatId = chatId;
        }

        public string UserName { get; }

        public long ChatId { get; }

        public static Result<TelegramUser> Create(string userName, long chatId)
        {
            return new TelegramUser(userName, chatId);
        }
    }
}