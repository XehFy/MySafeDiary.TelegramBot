using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace MySafeDiary.Infrastructure.Keyboards
{
    public static class Keyboard
    {
        public static ReplyKeyboardMarkup Registration = new ReplyKeyboardMarkup( new[]
        {
            new KeyboardButton[] {"Зарегистрироваться"}
        }) 
        { 
            ResizeKeyboard = true 
        };

        public static ReplyKeyboardMarkup Menu = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton[] {"Запись сейчас", "Запись в календарь"},
            new KeyboardButton[] {"Получить PDF дневника", "Прочитать записи за день"},
            new KeyboardButton[] {"Напомнить пароль"},
        })
        {
            ResizeKeyboard = true
        };
    }
}
