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
    public class GetNotesCommand : TelegramCommand
    {
        public override string Name => "Прочитать записи за день";

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);

            if (u == null) return;

            var userTo = new Data.Entities.User
            {
                Id = u.Id,
                IsNoteing = false,
                IsDateing = true,
                IsEmailing = false,
                IsPasswording = false,
                Email = u.Email,
                Password = u.Password
            };
            userRepository.Update(userTo);
            await userRepository.SaveAsync();

            var rm = new InlineKeyboardMarkup(Calendar.CreateCalendar((uint)DateTime.UtcNow.Year));
            await client.SendTextMessageAsync(message.Chat.Id, "🗓 <b>Выберете дату</b> 🗓", parseMode: ParseMode.Html, replyMarkup: rm);
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
    }
}
