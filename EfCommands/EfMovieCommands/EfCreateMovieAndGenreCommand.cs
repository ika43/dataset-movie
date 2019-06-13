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
    public class EfCreateMovieAndGenreCommand : BaseEfCommand, ICreateMovieAndGenreCommand
    {
        public EfCreateMovieAndGenreCommand(MovieContext ctx) : base(ctx) 
        {

        }

        public void Execute(CreateMovieAndGenreDto request)
        {
            if (Context.Movies.Any(p => p.Title.ToLower() == request.Title.ToLower()))
            {
                throw new EntityAlreadyExistException();
            }

            var genre = Context.Genres.Where(p => p.Name.ToLower() == request.Genre.Trim().ToLower()).FirstOrDefault();
            if (genre == null)
            {
                genre = new Domain.Genre
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Genre
                };
            }

            var movie = new Movie
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                ImdbRating = 5,
                Plot = request.Plot,
                Released = request.Released,
                Runtime = request.Runtime,
            };
            var genreMovie = new List<MovieGenre>();
            genreMovie.Add(new MovieGenre
            {
                MovieId = movie.Id,
                GenreId = genre.Id
            });
            movie.MovieGenres = genreMovie;
            Context.Movies.Add(movie);
            Context.SaveChanges();
        }
    }
}
