using Application.DTO;
using Application.Interfaces;

namespace Application.ICommands.IUserCommands
{
    public interface IUpdateUserCommand : ICommand<UserDto>
    {
    }
}
