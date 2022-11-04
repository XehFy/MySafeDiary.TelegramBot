using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Infrastructure.Keyboards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MySafeDiary.Domain.Commands
{
    public class RegisterCommand : TelegramCommand
    {
        public override string Name => "Зарегистрироваться";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

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
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите свой Email для связи и пин-код для доступа к записям в дневнике\nИспользуйте шаблон:");
                await botClient.SendTextMessageAsync(message.Chat.Id, "Регистрация\nexample@email.com\npassw0rd");
            }
        }
    }
}
