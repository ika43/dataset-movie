using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ICommands.IMovieCommands
{
    public interface IGetOneMovieCommand : ICommand<string, MovieDto>
    {
    }
}
