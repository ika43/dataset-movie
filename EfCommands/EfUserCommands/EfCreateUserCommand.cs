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
    public class EfCreateUserCommand : BaseEfCommand, ICreateUserCommand
    {
        public EfCreateUserCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(UserDto request)
        {
            if(Context.Users.Any(u=>u.Email.ToLower() == request.Email.Trim().ToLower()))
            {
                throw new EntityAlreadyExistException();
            }
            Context.Users.Add(new Domain.User
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email.Trim(),
            });
            Context.SaveChanges();
        }
    }
}
