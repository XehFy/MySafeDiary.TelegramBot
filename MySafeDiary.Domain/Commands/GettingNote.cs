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
using MySafeDiary.Infrastructure.Keyboards;
using Newtonsoft.Json.Converters;

namespace MySafeDiary.Domain.Commands
{
    public class GettingNote : NoTelegramCommand
    {
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var user = await userRepository.GetUserByIdAsync(message.Chat.Id);
            string note = message.Text;
            Data.Entities.User userDTO = new Data.Entities.User()
            {
                Email = user.Email,
                Id = user.Id,
                Password = user.Password,
                IsEmailing = user.IsEmailing,
                IsNoteing = false,
                IsPasswording = user.IsPasswording
            };
            userRepository.Update(userDTO);
            await userRepository.SaveAsync();
            var diaries = diaryRepository.FindAll();
            //var diary = await diaryRepository.GetDiaryByUserIdAsync(user.Id);
            //var did = diaries.First(d => d.UserId == user.Id).Id; new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds()
            Data.Entities.Note noteData = new Data.Entities.Note()
            {
                CreatedDate = message.Date + TimeSpan.FromHours(3),
                DiaryId = diaries.First(d => d.UserId == user.Id).Id,
                Text = note,
                Name = ""
            };
            noteRepository.AddNote(noteData, user);
            await noteRepository.SaveAsync();
            await client.SendTextMessageAsync(message.Chat.Id, "Запись успешно добавлена!", replyMarkup: Keyboard.Menu);
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
