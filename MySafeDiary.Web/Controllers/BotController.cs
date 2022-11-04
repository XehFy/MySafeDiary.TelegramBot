using System.Threading.Tasks;
using MySafeDiary.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using MySafeDiary.Data;
using MySafeDiary.Data.Repositories;
using System.Linq;

namespace MySafeDiary.Web.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;
        BotContext _botContext;

        public BotController(ICommandService commandService, ITelegramBotClient telegramBotClient, BotContext context)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
            _botContext = context;
            ContextInitialisator.initContext(context);
            TelegramCommand.initUserRepository(new UserRepository());
            TelegramCommand.initDiaryRepository(new DiaryRepository());
            //TelegramCommand.initContext(_botContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            string s = "";
            _botContext.Users.ToList().ForEach(p => s += p.Email + " " + p.Password + "\n");
            return Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();

            var message = update.Message;

            foreach (var command in _commandService.Get())
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, _telegramBotClient/*, _botContext*/);
                    break;
                }
            }
            return Ok();
        }
    }
}
