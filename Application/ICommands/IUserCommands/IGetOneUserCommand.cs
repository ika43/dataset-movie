using Application.DTO;
using Application.Interfaces;

namespace Application.ICommands.IUserCommands
{
    public interface IGetOneUserCommand : ICommand<string, UserDto>
    {
    }
}
