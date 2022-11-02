using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Domain.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MySafeDiary.Domain.Commands
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            ReplyKeyboardMarkup keyBoard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"Зарегистрироваться"}
            })
            { ResizeKeyboard = true};
            //IEmailService emailService = new EmailService();
            //emailService.Send("MySafeDiary@hotmail.com", "renat.churbanov@gmail.com", "Test Email Subject", "Example Plain Text Message Body");
            var mes = await botClient.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать!", replyMarkup: keyBoard);
            
        }
    }
}
