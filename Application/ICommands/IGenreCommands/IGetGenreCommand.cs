using Application.DTO;
using Application.Interfaces;
using Application.SearchObj;
using System.Collections.Generic;

namespace Application.ICommands.IGenreCommands
{
    public interface IGetGenreCommand : ICommand<GenreSearch, IEnumerable<GenreDto>>
    {
    }
}
