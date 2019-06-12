using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public abstract class BaseEfCommand
    {
        protected readonly MovieContext Context;
        protected BaseEfCommand(MovieContext context) => Context = context;
    }
}
