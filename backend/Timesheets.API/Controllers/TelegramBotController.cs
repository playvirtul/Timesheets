using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Timesheets.API.Controllers
{
    public class TelegramBotController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            var message = update.Message;
        }
    }
}