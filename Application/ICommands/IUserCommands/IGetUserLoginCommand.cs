using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ICommands.IUserCommands
{
    public interface IGetUserLoginCommand : ICommand<string, UserDto>
    {
    }
}
