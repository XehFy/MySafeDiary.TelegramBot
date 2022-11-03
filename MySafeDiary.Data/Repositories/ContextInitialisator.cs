using System;
using System.Collections.Generic;
using System.Text;

namespace MySafeDiary.Data.Repositories
{
    public abstract class ContextInitialisator
    {
        protected static BotContext BotContext { get; set; }

        public static void initContext(BotContext context)
        {
            BotContext = context;
        }
    }
}
