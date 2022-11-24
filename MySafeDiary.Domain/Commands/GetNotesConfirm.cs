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
using Telegram.Bot.Types.ReplyMarkups;

namespace MySafeDiary.Domain.Commands
{
    public class GetNotesConfirm : NoTelegramCommand
    {
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);
            
            if (u == null) return;

            if (u.Password == message.Text)
            {
                var rm = new InlineKeyboardMarkup(Calendar.CreateCalendar((uint)DateTime.UtcNow.Year));
                await client.SendTextMessageAsync(message.Chat.Id, "🗓 <b>Выберете дату</b> 🗓", parseMode: ParseMode.Html, replyMarkup: rm);
                await client.DeleteMessageAsync(chatId, message.MessageId);
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "Неправильный пароль", null);
            }

            
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;
            var user = userRepository.FindByCondition(u => u.Id == message.Chat.Id).FirstOrDefault();
            if (user == null) return false;
            return user.IsPasswording && user.IsDateing && !user.IsNoteing;
        }
    }
}
