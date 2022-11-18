using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Domain.Services;
using MySafeDiary.Infrastructure.Keyboards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySafeDiary.Domain.Commands
{
    public class GetPasswordCommand : TelegramCommand
    {
        public override string Name => "Напомнить пароль";

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var userTo = await userRepository.GetUserByIdAsync(message.Chat.Id);

            var email = new EmailService();
            email.Send("MySafeDiary@hotmail.com", userTo.Email, "Напоминание пароля", userTo.Password);

            await client.SendTextMessageAsync(message.Chat.Id, "Пароль был отправлен на вашу почту, указанную при регистрации", replyMarkup: Keyboard.Menu);
        }

        public override bool IsExecutionNeeded(Message message, ITelegramBotClient client)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
    }
}
