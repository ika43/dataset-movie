using Application.Exceptions;
using Application.ICommands.IUserCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfUserCommands
{
    public class EfDeleteUserCommand : BaseEfCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(string request)
        {
            var user = Context.Users.Find(request);
            if(user == null)
            {
                throw new EntityNotFoundException();
            }
            user.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
