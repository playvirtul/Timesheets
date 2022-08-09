using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Timesheets.Domain.Interfaces;
using Timesheets.Domain.Telegram;

namespace Timesheets.API.Controllers
{
    public class TelegramBotController : BaseController
    {
        private readonly ITelegramUsersService _telegramUsersService;
        private readonly ILogger _logger;

        public TelegramBotController(ITelegramUsersService telegramUsersService, ILogger<TelegramBotController> logger)
        {
            _telegramUsersService = telegramUsersService;
            _logger = logger;
        }

        [HttpPost("/TelegramUsers")]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    var telegramUser = TelegramUser.Create(message.Chat.Username, message.Chat.Id);

                    if (telegramUser.IsFailure)
                    {
                        _logger.LogError("{error}", telegramUser.Error);
                        return BadRequest(telegramUser.Error);
                    }

                    var telegramUserId = await _telegramUsersService.Create(telegramUser.Value);

                    if (telegramUserId == default)
                    {
                        return BadRequest("Не удалоь создать пользователя.");
                    }

                    return Ok(telegramUserId);
                }
            }

            return BadRequest();
        }
    }
}