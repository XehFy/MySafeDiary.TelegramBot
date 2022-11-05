using System;
using System.Collections.Generic;
using System.Text;

namespace MySafeDiary.Domain.Abstractions
{
    public interface ICommandService
    {
        List<INorTelegramCommand> Get();
    }
}
