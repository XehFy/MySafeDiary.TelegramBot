using System;
using System.Collections.Generic;
using System.Text;
using MySafeDiary.Domain.Abstractions;
using MySafeDiary.Domain.Commands;
using MySafeDiary.Data;

namespace MySafeDiary.Domain.Services
{
    public class CommandService : ICommandService
    {
        private readonly List<INorTelegramCommand> _commands;

        public CommandService()
        {
            _commands = new List<INorTelegramCommand>
            {
                new StartCommand(),
                new NoteNowCommand(),
                new NoteOnDateCommand(),
                new GetNotesCommand(),
                new GetPdfCommand(),
                new GetPasswordCommand(),
                new RegisterCommand(),
            };
        }

        public List<INorTelegramCommand> Get() => _commands;

    }
}
