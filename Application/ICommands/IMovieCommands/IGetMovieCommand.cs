using Application.DTO;
using Application.Interfaces;
using Application.SearchObj;
using System.Collections.Generic;

namespace Application.ICommands.IMovieCommands
{
    public interface IGetMovieCommand : ICommand<MovieSearch, IEnumerable<MovieDto>>
    {
    }
}
