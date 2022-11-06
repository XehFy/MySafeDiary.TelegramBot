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
                //new NullCatcher(),
                new StartCommand(),
                new NoteNowCommand(),
                new RegisterCommand(),
                new NewUserCommand()
            };
        }

        public List<INorTelegramCommand> Get() => _commands;

    }
}
