using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MySafeDiary.Infrastructure.Keyboards
{
    public static class Calendar
    {
        public static void OnCallbackQuery(CallbackQuery query, ITelegramBotClient bot)
        {
            var cbargs = query.Data.Split(' ');
            switch (cbargs[0])
            {
                case "month":
                    var month = new Month((MonthName)Enum.Parse(typeof(MonthName), cbargs[2]), uint.Parse(cbargs[1]));
                    var mkeyboard = new InlineKeyboardMarkup
                    (
                        CreateCalendar(month)
                    );
                    bot.EditMessageReplyMarkupAsync(query.Message.Chat.Id, query.Message.MessageId, mkeyboard);
                    break;
                case "year":
                    var ykeyboard = new InlineKeyboardMarkup
                    (
                        CreateCalendar(uint.Parse(cbargs[1]))
                    );
                    bot.EditMessageReplyMarkupAsync(query.Message.Chat.Id, query.Message.MessageId, ykeyboard);
                    break;
                default:
                    bot.AnswerCallbackQueryAsync(query.Id, query.Data, true);
                    break;
            }

        }

        public static InlineKeyboardButton[][] CreateCalendar(Month mon)
        {
            var calendar = new InlineKeyboardButton[mon.Weeks + 3][];
            var pos = 0;
            calendar[0] = new InlineKeyboardButton[1]
            {
                InlineKeyboardButton.WithCallbackData($"{mon.Name} {mon.Year}", $"year {mon.Year}")
            };
            var days = new[] { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
            calendar[1] = new InlineKeyboardButton[7];
            for (int i = 0; i < 7; i++)
            {
                calendar[1][i] = InlineKeyboardButton.WithCallbackData(days[i], $"{((DayName)i)}");
            }
            for (int i = 2; i < mon.Weeks + 2; i++)
            {
                calendar[i] = new InlineKeyboardButton[7];
                for (int j = 0; j < 7; j++)
                {
                    if (pos < mon.Days.Length)
                    {
                        if ((int)mon.Days[pos].Name == j)
                        {
                            //calendar[i][j] = InlineKeyboardButton.WithCallbackData($"{mon.Days[pos].Number}", $"{mon.Days[pos].Name}, {(int)mon.Name + 1} {mon.Days[pos].Number}, {mon.Year}");
                            calendar[i][j] = InlineKeyboardButton.WithCallbackData($"{mon.Days[pos].Number}", $"{(int)mon.Name + 1}/{mon.Days[pos].Number}/{mon.Year}");
                            pos++;
                        }
                        else
                        {
                            calendar[i][j] = InlineKeyboardButton.WithCallbackData("*", "Empty day");
                        }
                    }
                    else
                    {
                        calendar[i][j] = InlineKeyboardButton.WithCallbackData("*", "Empty day");
                    }
                }
            }
            calendar[calendar.Length - 1] = new InlineKeyboardButton[2];
            var previousmonth = mon.Name == MonthName.January ? MonthName.December : mon.Name - 1;
            var nextmonth = mon.Name == MonthName.December ? MonthName.January : mon.Name + 1;
            var previousyear = previousmonth == MonthName.December ? mon.Year - 1 : mon.Year;
            var nextyear = nextmonth == MonthName.January ? mon.Year + 1 : mon.Year;
            calendar[calendar.Length - 1][0] = InlineKeyboardButton.WithCallbackData($"{previousmonth}", $"month {previousyear} {((ushort)previousmonth)}");
            calendar[calendar.Length - 1][1] = InlineKeyboardButton.WithCallbackData($"{nextmonth}", $"month {nextyear} {((ushort)nextmonth)}");
            return calendar;
        }
        public static InlineKeyboardButton[][] CreateCalendar(uint year)
        {
            var keyboard = new InlineKeyboardButton[6][];
            keyboard[0] = new InlineKeyboardButton[1]{
                InlineKeyboardButton.WithCallbackData($"{year}", $"Year {year}")
            };
            for (int i = 1, n = 0; i < 5; i++)
            {
                keyboard[i] = new InlineKeyboardButton[3];
                for (int j = 0; j < 3; j++, n++)
                {
                    var month = (MonthName)n;
                    keyboard[i][j] = InlineKeyboardButton.WithCallbackData($"{month}", $"month {year} {n}");
                }
            }
            keyboard[5] = new InlineKeyboardButton[2]{
                InlineKeyboardButton.WithCallbackData($"{year - 1}",$"year {year - 1}"),
                InlineKeyboardButton.WithCallbackData($"{year + 1}",$"year {year + 1}")
            };
            return keyboard;
        }
    }
    public enum DayName
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public class Day
    {
        public Day(DayName name, ushort number)
        {
            Name = name; Number = number;
        }
        public DayName Name { get; set; }
        public ushort Number { get; set; }
    }
    public enum MonthName
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    public class Month
    {
        public Month(MonthName monthName, uint year)
        {
            Name = monthName;
            Year = year;
            var leapyear = Year % 4 == 0;
            var days = Name == MonthName.February ? (leapyear ? 29 : 28) : (Name == MonthName.April || Name == MonthName.June || Name == MonthName.September || Name == MonthName.November ? 30 : 31);
            Days = new Day[days];
            var firstday = year * 365 + (leapyear ? -1 : 0) + (((year - (year % 4)) / 4)) - (((year - (year % 400)) / 400)) + 3;
            var month = (int)monthName;
            firstday += month < 1 ? 0 : 31;
            firstday += month < 2 ? 0 : (leapyear ? 29 : 28);
            firstday += month < 3 ? 0 : 31;
            firstday += month < 4 ? 0 : 30;
            firstday += month < 5 ? 0 : 31;
            firstday += month < 6 ? 0 : 30;
            firstday += month < 7 ? 0 : 31;
            firstday += month < 8 ? 0 : 31;
            firstday += month < 9 ? 0 : 30;
            firstday += month < 10 ? 0 : 31;
            firstday += month < 11 ? 0 : 30;
            firstday = firstday % 7;
            for (int i = 0; i < Days.Length; i++)
                Days[i] = new Day((DayName)((i + firstday) % 7), (ushort)(i + 1));
        }
        public uint Year { get; set; }
        public MonthName Name { get; set; }
        public Day[] Days { get; set; }
        public ushort Weeks
        {
            get
            {
                var days = (int)Days[0].Name + Days.Length - 1;
                return (ushort)(((days - (days % 7)) / 7) + (days % 7 > 0 ? 1 : 0));
            }
        }
    }
}
