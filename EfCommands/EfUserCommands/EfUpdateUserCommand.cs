using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IUserCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUserCommands
{
    public class EfUpdateUserCommand : BaseEfCommand, IUpdateUserCommand
    {
        public EfUpdateUserCommand(MovieContext ctx) : base(ctx) { }

        public void Execute(UserDto request)
        {
            var user = Context.Users.Find(request.Id);
            if (user == null || user.IsDeleted) throw new EntityNotFoundException();
            if (Context.Users.Any(p => p.Email.ToLower() == request.Email.Trim().ToLower())) throw new EntityAlreadyExistException();
            user.Email = request.Email;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            Context.SaveChanges();
        }
    }
}
