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
        private readonly List<TelegramCommand> _commands;

        public CommandService()
        {
            _commands = new List<TelegramCommand>
            {
                new StartCommand(),
                new RegisterCommand(),
                new NewUserCommand()
            };
        }

        public List<TelegramCommand> Get() => _commands;

    }
}
