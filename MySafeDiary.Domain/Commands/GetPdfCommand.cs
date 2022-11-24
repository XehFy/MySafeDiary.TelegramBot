using MySafeDiary.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySafeDiary.Domain.Commands
{
    public class GetPdfCommand : TelegramCommand
    {
        public override string Name => "Получить PDF дневника";

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);

            if (u == null) return;

            var userTo = new Data.Entities.User
            {
                Id = u.Id,
                IsNoteing = true,
                IsDateing = true,
                IsEmailing = false,
                IsPasswording = true,
                Email = u.Email,
                Password = u.Password
            };
            userRepository.Update(userTo);
            await userRepository.SaveAsync();

            await client.SendTextMessageAsync(message.Chat.Id, "Введите пин-код", null);
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
    }
}
