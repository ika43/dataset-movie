using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IGenreCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfGenreCommands
{
    public class EfUpdateGenreCommand : BaseEfCommand, IUpdateGenreCommand
    {
        public EfUpdateGenreCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(GenreDto request)
        {
            var genre = Context.Genres.Find(request.Id);
            if(genre == null)
            {
                throw new EntityNotFoundException();
            }
            if(genre.Name == request.Name)
            {
                throw new EntityAlreadyExistException();
            }
            genre.Name = request.Name;
            genre.ModifiedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
