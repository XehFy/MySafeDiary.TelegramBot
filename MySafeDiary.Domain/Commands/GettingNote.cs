using MySafeDiary.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MySafeDiary.Data.Entities;
using System.Linq;
using Telegram.Bot.Types.Enums;

namespace MySafeDiary.Domain.Commands
{
    public class GettingNote : NoTelegramCommand
    {
        public override Task Execute(Message message, ITelegramBotClient client)
        {
            throw new NotImplementedException();
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;
            var user = userRepository.FindByCondition(u => u.Id == message.Chat.Id).FirstOrDefault();
            if (user == null) return false;
            return user.IsNoteing;
        }
    }
}
