using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Domain.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using MySafeDiary.Data.Entities;
using MySafeDiary.Infrastructure.Keyboards;
using System.Linq;

namespace MySafeDiary.Domain.Commands
{
    public class StartCommand : TelegramCommand
    {

        public override string Name => @"/start";

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            try
            {
                if (message.Type != MessageType.Text)
                    return false;
            }
            catch { };
            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);
            if (u != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы уже зарегистрированы " + u.Email, replyMarkup: Keyboard.Menu);
            }
            else
            {
                u = new Data.Entities.User
                {
                    Id = chatId
                };
                userRepository.CreateUser(u);
                await userRepository.SaveAsync();
                var mes = await botClient.SendTextMessageAsync(message.Chat.Id, "Здесь будет указана вся инфа и инструкции по боту", replyMarkup: Keyboard.Registration);
            }  
            
        }
    }
}
