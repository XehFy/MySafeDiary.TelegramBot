using System;
using System.Collections.Generic;
using System.Text;
using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Domain.Commands;

namespace MySafeDiary.Domain.Services
{
    public class NoCommandService : ICommandService
    {
        private readonly List<INorTelegramCommand> _noCommands;

        public NoCommandService()
        {
            _noCommands = new List<INorTelegramCommand>
            {
                new GettingNote(),
                new GettingNoteWithDate(),
                new GetNotesConfirm(),
                new GetPdfConfirm(),
                new GettingEmail(),
                new GettingPassword()
            };
        }

        public List<INorTelegramCommand> Get() => _noCommands;
    }
}
