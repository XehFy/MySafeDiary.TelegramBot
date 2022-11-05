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
    public class GettingEmail : NoTelegramCommand
    {
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var user = await userRepository.GetUserByIdAsync(message.Chat.Id);
            string email = message.Text.Trim();
            Data.Entities.User userDTO = new Data.Entities.User()
            {
                Email = email,
                Id = user.Id,
                Password = user.Password,
                IsEmailing = false,
                IsNoteing = user.IsNoteing,
                IsPasswording = true
                };
            userRepository.Update(userDTO);
            await userRepository.SaveAsync();
            await client.SendTextMessageAsync(message.Chat.Id, "Введите пароль для доступа к записям дневника");
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;
            var user = userRepository.FindByCondition(u => u.Id == message.Chat.Id).FirstOrDefault();
            if (user == null) return false;
            return user.IsEmailing;
        }
    }
}
