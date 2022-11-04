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
using MySafeDiary.Data.Entities;
using MySafeDiary.Infrastructure.Keyboards;
using System.Linq;

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

        public override async Task Execute(Message message, ITelegramBotClient botClient/*, BotContext botContext*/)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);
            if (u != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы уже зарегистрированы " + u.Email, replyMarkup: Keyboard.Menu);
            }
            else
            {
                /*ReplyKeyboardMarkup keyBoard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Зарегистрироваться"}
                })
                { ResizeKeyboard = true };*/
                var mes = await botClient.SendTextMessageAsync(message.Chat.Id, "Здесь будет указана вся инфа и инструкции по боту", replyMarkup: Keyboard.Registration);
            }
            
            
            //IEmailService emailService = new EmailService();
            //emailService.Send("MySafeDiary@hotmail.com", "renat.churbanov@gmail.com", "Test Email Subject", "Example Plain Text Message Body");
            
            //botContext.Users.ToList().ForEach(p => s += p.Email + " " + p.Password + "\n");
            
            
        }
    }
}
