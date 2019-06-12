using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieContext db = new MovieContext();
            //var genre = new Genre
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Adventure"
            //};
            //db.Genres.Add(genre);
            //var genre = db.Genres.First().Id;
            //var movie = new Movie
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    ImdbRating = 9.8,
            //    Title = "ASP NET MOVIE",
            //    Plot = "Lorem ipsum dolor sit amet",
            //    Runtime = 90,
            //    Released = DateTime.Now,
            //    MovieGenres = new List<MovieGenre>
            //    {
            //        new MovieGenre
            //        {

            //        }
            //    }
            //};
            //db.MovieGenres.Add(new MovieGenre
            //{
            //    GenreId = genre,
            //    MovieId = movie.Id
            //});
            //db.Movies.Add(movie);
            //db.SaveChanges();
            // movie.MovieGenres.Add();
            var data = db.Movies.Select(m => new
            {
                id = m.Id,
                title = m.Title,
                genres = m.MovieGenres.Select(p => new
                {
                    name = p.Genre.Name
                }),
                comments = m.Comments.Select(c => new
                {
                    text = c.Text,
                    created = c.CreatedAt
                })
            });
            Console.WriteLine("ENDE");
        }
    }
}
