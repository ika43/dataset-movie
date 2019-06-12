using Application.DTO;
using Application.Interfaces;

namespace Application.ICommands.IMovieCommands
{
    public interface ICreateMovieCommand : ICommand<MovieDto>
    {
    }
}
