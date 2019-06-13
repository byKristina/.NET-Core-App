using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public abstract class BaseEfCommand
    {
        protected BlogContext Context { get; }

        protected BaseEfCommand(BlogContext context) => Context = context;
    }
}
