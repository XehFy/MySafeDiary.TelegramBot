using MySafeDiary.Data.Entities;
using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Infrastructure.Keyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MySafeDiary.Domain.Commands
{
    public class OnCallbackQuery : RepositoryInitializator
    {
        public async Task CallbackQueryHandle(CallbackQuery query, ITelegramBotClient bot)
        {
            var cbargs = query.Data.Split(' ');
            var chatId = query.Message.Chat.Id;

            var u = await userRepository.GetUserByIdAsync(chatId);
            if (u == null) return;

            switch (cbargs[0])
            {
                case "month":
                    var month = new Month((MonthName)Enum.Parse(typeof(MonthName), cbargs[2]), uint.Parse(cbargs[1]));
                    var mkeyboard = new InlineKeyboardMarkup
                    (
                        Calendar.CreateCalendar(month)
                    );
                    await bot.EditMessageReplyMarkupAsync(query.Message.Chat.Id, query.Message.MessageId, mkeyboard);
                    break;
                case "year":
                    var ykeyboard = new InlineKeyboardMarkup
                    (
                        Calendar.CreateCalendar(uint.Parse(cbargs[1]))
                    );
                    await bot.EditMessageReplyMarkupAsync(query.Message.Chat.Id, query.Message.MessageId, ykeyboard);
                    break;
                default:

                    if (u.IsNoteing && u.IsDateing) {

                        var userTo = new Data.Entities.User
                        {
                            Id = u.Id,
                            IsNoteing = true,
                            IsDateing = true,
                            IsEmailing = u.IsEmailing,
                            IsPasswording = u.IsPasswording,
                            Email = u.Email,
                            Password = u.Password
                        };

                        userRepository.Update(userTo);
                        await userRepository.SaveAsync();

                        var diaries = diaryRepository.FindAll();
                        Data.Entities.Note noteData = new Data.Entities.Note()
                        {
                            CreatedDate = DateTime.ParseExact(query.Data, "M/d/yyyy", null),
                            DiaryId = diaries.First(d => d.UserId == u.Id).Id,
                            Text = "",
                            Name = ""
                        };
                        noteRepository.AddNote(noteData, userTo);
                        await noteRepository.SaveAsync();
                        await bot.SendTextMessageAsync(chatId, "Введите запись");
                    }

                    else if (u.IsPasswording && u.IsDateing) {

                        var userTo = new Data.Entities.User
                        {
                            Id = u.Id,
                            IsNoteing = false,
                            IsDateing = false,
                            IsEmailing = false,
                            IsPasswording = false,
                            Email = u.Email,
                            Password = u.Password
                        };

                        userRepository.Update(userTo);
                        await userRepository.SaveAsync();

                        var diaries = diaryRepository.FindAll();
                        var diaryId = diaries.First(d => d.UserId == u.Id).Id;

                        var notes = noteRepository.FindByCondition(c => c.DiaryId == diaryId).OrderBy(c => c.CreatedDate).Where(c => c.CreatedDate.Date == DateTime.ParseExact(query.Data, "M/d/yyyy", null));
                        if (!notes.Any())
                        {
                            await bot.SendTextMessageAsync(chatId, "У вас не было записей за этот день", null);
                        }
                        else
                        {
                            foreach (var note in notes)
                            {
                                await bot.SendTextMessageAsync(chatId, note.CreatedDate.ToString() + "\n" + note.Text, null);
                            }
                        }

                    }
                    await bot.EditMessageTextAsync(chatId, query.Message.MessageId, query.Data, replyMarkup: null);
                    break;
            }

        }
    }
}
