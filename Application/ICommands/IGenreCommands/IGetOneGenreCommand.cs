using Application.DTO;
using Application.Interfaces;

namespace Application.ICommands.IGenreCommands
{
    public interface IGetOneGenreCommand : ICommand<string, GenreDto>
    {
    }
}
