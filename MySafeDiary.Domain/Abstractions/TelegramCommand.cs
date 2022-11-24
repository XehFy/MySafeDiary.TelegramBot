using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MySafeDiary.Data;
using MySafeDiary.Data.Repositories;

namespace MySafeDiary.Domain.Abstractions
{
    public abstract class TelegramCommand : RepositoryInitializator, INorTelegramCommand
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, ITelegramBotClient client);

        public abstract bool IsExecutionNeeded(Message message, ITelegramBotClient client);
    }
}
