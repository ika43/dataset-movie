using Application.DTO;
using Application.ICommands.IUserCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfUserCommands
{
    public class EfGetLoginUserCommand : BaseEfCommand, IGetUserLoginCommand
    {
        public EfGetLoginUserCommand(MovieContext ctx) : base(ctx)
        {

        }

        public UserDto Execute(string request)
        {
            var user = Context.Users
                .Where(p => p.Email.ToLower() == request.Trim().ToLower())
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    CreatedAt = u.CreatedAt,
                })
                .FirstOrDefault();
            return user;
        }
    }
}
