using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IGenreCommands;
using Domain;
using EfDataAccess;
using System;
using System.Linq;

namespace EfCommands.EfGenreCommands
{
    public class EfCreateGenreCommand : BaseEfCommand, ICreateGenreCommand
    {
        public EfCreateGenreCommand(MovieContext ctx) : base(ctx) { }

        public void Execute(GenreDto request)
        {
            if (Context.Genres.Any(g => g.Name == request.Name))
            {
                throw new EntityAlreadyExistException();
            }
            var genre = new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
            };
            Context.Genres.Add(genre);
            Context.SaveChanges();
        }
    }
}
