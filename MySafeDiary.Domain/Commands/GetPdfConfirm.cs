using MySafeDiary.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using IronPdf;
using Telegram.Bot.Types.InputFiles;
using System.IO;

namespace MySafeDiary.Domain.Commands
{
    public class GetPdfConfirm : NoTelegramCommand
    {
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var u = await userRepository.GetUserByIdAsync(chatId);

            if (u == null) return;

            if (u.Password == message.Text)
            {
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

                var notes = noteRepository.FindByCondition(n => n.DiaryId == diaryId).OrderBy(n => n.CreatedDate);

                StringBuilder diaryHtml = new StringBuilder();
                foreach (var note in notes)
                {
                    diaryHtml.Append("<h2>" + note.CreatedDate + "</h2>\n");
                    diaryHtml.Append(note.Text + "\n");
                }

                var Renderer = new ChromePdfRenderer();
                var pdf = await Renderer.RenderHtmlAsPdfAsync(diaryHtml.ToString());

                pdf.SaveAs($"{chatId}.pdf");

                await using Stream stream = System.IO.File.OpenRead($"..\\MySafeDiary.Web\\{chatId}.pdf");
                await client.SendDocumentAsync(
                    chatId: chatId,
                    document: new InputOnlineFile(content: stream, fileName: $"{ chatId }.pdf"));

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

            return user.IsPasswording && user.IsDateing && user.IsNoteing;
        }
    }
}
