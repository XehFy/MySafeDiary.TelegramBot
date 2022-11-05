using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MySafeDiary.Domain.Abstractions
{
    public interface INorTelegramCommand
    {
        Task Execute(Message message, ITelegramBotClient client);
        bool IsExecutionNeeded(Message message, ITelegramBotClient client);
    }
}
