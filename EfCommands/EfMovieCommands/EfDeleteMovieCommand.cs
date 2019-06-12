using Application.Exceptions;
using Application.ICommands.IMovieCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfMovieCommands
{
    public class EfDeleteMovieCommand : BaseEfCommand, IDeleteMovieCommand
    {
        public EfDeleteMovieCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(string request)
        {
            var movie = Context.Movies.Find(request);
            if (movie == null || movie.IsDeleted) throw new EntityNotFoundException();
            movie.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
