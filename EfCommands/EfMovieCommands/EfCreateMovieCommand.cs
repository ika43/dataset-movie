using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IMovieCommands;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfMovieCommands
{
    public class EfCreateMovieCommand : BaseEfCommand, ICreateMovieCommand
    {
        public EfCreateMovieCommand(MovieContext ctx) : base(ctx) { }

        public void Execute(MovieDto request)
        {
            if (Context.Movies.Any(p => p.Title.ToLower() == request.Title.ToLower()))
            {
                throw new EntityAlreadyExistException();
            }
            var movie = new Movie
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Runtime = request.Runtime,
                ImdbRating = request.ImdbRating,
                Plot = request.Plot,
                Released = request.Released
            };
            var genreMovie = new List<MovieGenre>();
            foreach (var genre in request.Genres)
            {
                genreMovie.Add(new MovieGenre
                {
                    Genre = Context.Genres.Where(g=>g.Name.ToLower() == genre.Name.Trim().ToLower()).FirstOrDefault(),
                    MovieId = movie.Id
                });
            }
            movie.MovieGenres = genreMovie;
            Context.Movies.Add(movie);
            Context.SaveChanges();
        }
    }
}
