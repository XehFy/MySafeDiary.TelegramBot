using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Infrastructure.Keyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySafeDiary.Domain.Commands
{
    public class GettingNoteWithDate : NoTelegramCommand
    {
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var user = await userRepository.GetUserByIdAsync(message.Chat.Id);
            string noteText = message.Text;

            Data.Entities.User userDTO = new Data.Entities.User()
            {
                Email = user.Email,
                Id = user.Id,
                Password = user.Password,
                IsEmailing = user.IsEmailing,
                IsNoteing = false,
                IsDateing = false,
                IsPasswording = user.IsPasswording
            };

            userRepository.Update(userDTO);
            await userRepository.SaveAsync();

            var diary = diaryRepository.FindByCondition(d => d.UserId == user.Id).FirstOrDefault();
            var notes = noteRepository.FindByCondition(n => n.DiaryId == diary.Id);
            var note = notes.OrderByDescending(n => n.Id).FirstOrDefault();

            Data.Entities.Note noteData = new Data.Entities.Note()
            {
                Id = note.Id,
                CreatedDate = note.CreatedDate,
                DiaryId = diary.Id,
                Text = noteText,
                Name = ""
            };
            noteRepository.Update(noteData);

            await noteRepository.SaveAsync();

            await client.SendTextMessageAsync(message.Chat.Id, "Запись успешно добавлена!", replyMarkup: Keyboard.Menu);

            await client.DeleteMessageAsync(message.Chat.Id, message.MessageId);
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;
            var user = userRepository.FindByCondition(u => u.Id == message.Chat.Id).FirstOrDefault();
            if (user == null) return false;
            return user.IsNoteing && user.IsDateing && !user.IsPasswording;
        }
    }
}
