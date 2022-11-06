using MySafeDiary.Domain.Abstractions;
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
    public class NoteNowCommand : TelegramCommand
    {
        public override string Name => "Запись сейчас";

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);
            if (u == null) return;
            u = new Data.Entities.User
            {
                Id = message.Chat.Id,
                IsNoteing = true,
                IsEmailing = u.IsEmailing,
                IsPasswording = u.IsPasswording,
                Email = u.Email,
                Password = u.Password
            };
            userRepository.Update(u);
            await userRepository.SaveAsync();
            await client.SendTextMessageAsync(message.Chat.Id, "Введи запись, которая будет сохранена на текущие дату и время", replyMarkup: new ReplyKeyboardRemove());
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
    }
}
