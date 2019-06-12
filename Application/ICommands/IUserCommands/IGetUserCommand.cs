using Application.DTO;
using Application.Interfaces;
using Application.SearchObj;
using System.Collections.Generic;

namespace Application.ICommands.IUserCommands
{
    public interface IGetUserCommand : ICommand<UserSearch, IEnumerable<UserDto>>
    {
    }
}
