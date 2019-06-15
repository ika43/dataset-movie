using Application.Exceptions;
using Application.ICommands.IGenreCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfGenreCommands
{
    public class EfDeleteGenreCommand : BaseEfCommand, IDeleteGenreCommand
    {
        public EfDeleteGenreCommand(MovieContext ctx) : base(ctx) { }

        public void Execute(string request)
        {
            var genre = Context.Genres.Find(request);
            if(genre == null)
            {
                throw new EntityNotFoundException();
            }
            genre.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
