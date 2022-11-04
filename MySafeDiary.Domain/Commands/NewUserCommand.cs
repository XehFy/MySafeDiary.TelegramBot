using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MySafeDiary.Infrastructure.Keyboards;

namespace MySafeDiary.Domain.Commands
{
    public class NewUserCommand : TelegramCommand
    {
        public override string Name => "Регистрация";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var user = await userRepository.GetUserByIdAsync(chatId);
            if (user != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы уже зарегистрированы " + user.Email);
            }
            else
            {
                string messageText = message.Text;
                string email = messageText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[1];
                string password = messageText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[2];
                user = new Data.Entities.User
                {
                    Id = chatId,
                    Password = password,
                    Email = email
                };
                var diary = new Data.Entities.Diary { Name = "Мой дневник" };
                userRepository.CreateUser(user);
                userRepository.SaveAsync();
                //await userRepository.SaveAsync();
                diaryRepository.AddDiary(diary, user);
                //diaryRepository.AddDiary(diary, user);
                //await diaryRepository.SaveAsync();
                await userRepository.SaveAsync();
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы зарегистрированы успешно!", replyMarkup: Keyboard.Menu);
            }
        }
    }
}
