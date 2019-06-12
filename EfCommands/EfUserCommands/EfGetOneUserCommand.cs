using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IUserCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfUserCommands
{
    public class EfGetOneUserCommand : BaseEfCommand, IGetOneUserCommand
    {
        public EfGetOneUserCommand(MovieContext ctx) : base(ctx)
        {

        }

        public UserDto Execute(string request)
        {
            var user = Context.Users.Find(request);
            if(user == null || user.IsDeleted)
            {
                throw new EntityNotFoundException();
            }
            return new UserDto
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
            };
        }
    }
}
