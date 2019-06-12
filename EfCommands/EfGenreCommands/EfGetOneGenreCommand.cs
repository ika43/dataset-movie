using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IGenreCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfGenreCommands
{
    public class EfGetOneGenreCommand : BaseEfCommand, IGetOneGenreCommand
    {
        public EfGetOneGenreCommand(MovieContext ctx) : base(ctx) { }

        public GenreDto Execute(string request)
        {
            var genre = Context.Genres.Find(request);
            if(genre == null)
            {
                throw new EntityNotFoundException();
            }
            return new GenreDto
            {
                Id = genre.Id,
                CreatedAt = genre.CreatedAt,
                Name = genre.Name
            };
        }
    }
}
