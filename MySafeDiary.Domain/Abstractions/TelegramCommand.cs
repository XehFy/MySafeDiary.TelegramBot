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
    public abstract class TelegramCommand
    {
        //protected static BotContext botContext;
        protected static UserRepository _userRepository;

        public abstract string Name { get; }

        public abstract Task Execute(Message message, ITelegramBotClient client/*, BotContext botContext*/);

        public abstract bool Contains(Message message);

        public static void initUserRepository(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
