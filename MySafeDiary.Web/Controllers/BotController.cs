using System.Threading.Tasks;
using MySafeDiary.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using MySafeDiary.Data;
using MySafeDiary.Data.Repositories;
using System.Linq;
using System.Collections.Generic;
using MySafeDiary.Domain.Services;
using Telegram.Bot.Types.Enums;
using MySafeDiary.Infrastructure.Keyboards;

namespace MySafeDiary.Web.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;
        private readonly ICommandService _noCommandService;

        //BotContext _botContext;

        public BotController(IEnumerable<ICommandService> commandServices, ITelegramBotClient telegramBotClient, BotContext context)
        {
            _commandService = commandServices.First(o => o.GetType() == typeof(CommandService));
            _noCommandService = commandServices.First(o => o.GetType() == typeof(NoCommandService));
            _telegramBotClient = telegramBotClient;
            //_botContext = context;
            ContextInitialisator.initContext(context);
            RepositoryInitializator.initUserRepository(new UserRepository());
            RepositoryInitializator.initDiaryRepository(new DiaryRepository());
            RepositoryInitializator.initNoteRepository(new NoteRepository());
            //TelegramCommand.initUserRepository(new UserRepository());
            //TelegramCommand.initDiaryRepository(new DiaryRepository());
            //TelegramCommand.initContext(_botContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Bot started");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            CommandService commandService = (CommandService)_commandService;
            NoCommandService noCommandService = (NoCommandService)_noCommandService;
            if (update == null) return Ok();
            var message = update.Message;
            bool IsCommand = false;

            if (update.Type == UpdateType.CallbackQuery)
            {
                Calendar.OnCallbackQuery(update.CallbackQuery, _telegramBotClient);
                return Ok();
            }

            if (message == null)
            {
                return Ok();
            }

            foreach (TelegramCommand command in commandService.Get())
            {
                if (command.IsExecutionNeeded(message, _telegramBotClient))
                {
                    IsCommand = true;
                    await command.Execute(message, _telegramBotClient);
                    break;
                }
            }
            if (!IsCommand)
            {
                foreach (INorTelegramCommand command in noCommandService.Get())
                {
                    if (command.IsExecutionNeeded(message, _telegramBotClient))
                    {
                        IsCommand = true;
                        await command.Execute(message, _telegramBotClient);
                        break;
                    }
                }
                //await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            return Ok();
        }
    }
}
