using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IMovieCommands;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfMovieCommands
{
    public class EfUpdateMovieCommand : BaseEfCommand, IUpdateMovieCommand
    {
        public EfUpdateMovieCommand(MovieContext ctx) : base(ctx)
        {
                
        }

        public void Execute(MovieDto request)
        {
            var movie = Context.Movies.Find(request.Id);
            if (movie == null || movie.IsDeleted) throw new EntityNotFoundException();
            if (Context.Movies.Any(p => p.Title.ToLower() == request.Title.Trim().ToLower())) throw new EntityAlreadyExistException();
            movie.Title = request.Title;
            movie.Released = request.Released;
            movie.Plot = request.Plot;
            //var genreMovie = new List<MovieGenre>();
            //foreach (var genre in request.Genres)
            //{
            //    genreMovie.Add(new MovieGenre
            //    {
            //        GenreId = genre.Id,
            //        MovieId = movie.Id
            //    });
            //}
            // movie.MovieGenres = genreMovie;
            movie.ModifiedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
