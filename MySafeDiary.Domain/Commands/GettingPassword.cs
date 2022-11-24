using MySafeDiary.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using MySafeDiary.Data.Entities;
using System.Linq;
using MySafeDiary.Infrastructure.Keyboards;
using Telegram.Bot.Types.Enums;

namespace MySafeDiary.Domain.Commands
{
    public class GettingPassword : NoTelegramCommand
    {
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var user = await userRepository.GetUserByIdAsync(message.Chat.Id);
            string password = message.Text.Trim();
            Data.Entities.User userDTO = new Data.Entities.User()
            {
                Email = user.Email,
                Id = user.Id,
                Password = password,
                IsEmailing = user.IsEmailing,
                IsNoteing = user.IsNoteing,
                IsPasswording = false
            };
            userRepository.Update(userDTO);
            await userRepository.SaveAsync();

            var diary = new Data.Entities.Diary { Name = "Мой дневник" };
            diaryRepository.AddDiary(diary, user);
            await userRepository.SaveAsync();
            
            await client.SendTextMessageAsync(message.Chat.Id, "Вы успешно зарегестрированы!\nТеперь можете приступить к ведению дневника.", replyMarkup: Keyboard.Menu);

            await client.DeleteMessageAsync(message.Chat.Id, message.MessageId);
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;
            
            var user = userRepository.FindByCondition(u => u.Id == message.Chat.Id).FirstOrDefault();
            
            if (user == null) return false;
            
            return user.IsPasswording && !user.IsDateing;
        }
    }
}