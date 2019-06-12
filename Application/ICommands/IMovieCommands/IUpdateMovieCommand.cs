using Application.DTO;
using Application.Interfaces;

namespace Application.ICommands.IMovieCommands
{
    public interface IUpdateMovieCommand : ICommand<MovieDto>
    {
    }
}
