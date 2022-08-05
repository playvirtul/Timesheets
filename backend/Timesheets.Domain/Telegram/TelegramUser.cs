using CSharpFunctionalExtensions;

namespace Timesheets.Domain.Telegram
{
    public class TelegramUser
    {
        private TelegramUser(string userName, string chatId)
        {
            UserName = userName;
            ChatId = chatId;
        }

        public string UserName { get; set; }

        public string ChatId { get; set; }

        public static Result<TelegramUser> Create(string userName, string chatId)
        {
            return new TelegramUser(userName, chatId);
        }
    }
}