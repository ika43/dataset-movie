using Application.DTO;
using Application.ICommands.IUserCommands;
using Application.SearchObj;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EfCommands.EfUserCommands
{
    public class EfGetUserCommand : BaseEfCommand, IGetUserCommand
    {
        public EfGetUserCommand(MovieContext ctx) : base(ctx)
        {

        }

        public IEnumerable<UserDto> Execute(UserSearch request)
        {
            var query = Context.Users.AsQueryable();

            if(request.Email != null)
            {
                query = query.Where(p => p.Email.ToLower().Contains(request.Email.Trim().ToLower()));
            }

            if (request.Firstname != null)
            {
                query = query.Where(p => p.Firstname.ToLower().Contains(request.Firstname.Trim().ToLower()));
            }

            if (request.Lastname != null)
            {
                query = query.Where(p => p.Lastname.ToLower().Contains(request.Lastname.Trim().ToLower()));
            }

            if (request.IsDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == request.IsDeleted);
            }
            else
            {
                query = query.Where(p => p.IsDeleted == false);
            }

            return query.Select(p => new UserDto
            {
                Id = p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Email = p.Email,
                CreatedAt = p.CreatedAt
            });
        }
    }
}
