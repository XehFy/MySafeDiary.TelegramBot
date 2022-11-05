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
        //protected static BotContext botContext;
        //protected static UserRepository userRepository;
        //public static DiaryRepository diaryRepository;

        public abstract string Name { get; }

        public abstract Task Execute(Message message, ITelegramBotClient client/*, BotContext botContext*/);

        public abstract bool IsExecutionNeeded(Message message, ITelegramBotClient client);

        /*public static void initUserRepository(UserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public static void initDiaryRepository(DiaryRepository _diaryRepository)
        {
            diaryRepository = _diaryRepository;
        }*/
    }
}
