using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MySafeDiary.Domain.Abstractions
{
    public abstract class TelegramCommand
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, ITelegramBotClient client);

        public abstract bool Contains(Message message);
    }
}
