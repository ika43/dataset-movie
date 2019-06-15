using Application.DTO;
using Application.Exceptions;
using Application.ICommands.IMovieCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfMovieCommands
{
    public class EfGetOneMovieCommand : BaseEfCommand, IGetOneMovieCommand
    {
        public EfGetOneMovieCommand(MovieContext ctx) : base(ctx)
        {

        }

        public MovieDto Execute(string request)

        {
            // var movie = Context.Movies.Find(request);
            var query = Context.Movies.AsQueryable();
            query = query.Where(p => p.Id == request);
            if(!query.Any() || query.Any(p=>p.IsDeleted)) throw new EntityNotFoundException();
            return query.Select(p => new MovieDto
            {
                Id = p.Id,
                Title = p.Title,
                Plot = p.Plot,
                Released = p.Released,
                Runtime = p.Runtime,
                CreatedAt = p.CreatedAt,
                ImdbRating = p.ImdbRating,
                Comments = p.Comments.Select(c => new CommentDto
                {
                    Text = c.Text,
                    User = c.User.Email,
                    CreatedAt = c.CreatedAt
                }).ToList(),
                Genres = p.MovieGenres.Select(g => new GenreDto
                {
                    Name = g.Genre.Name,
                    Id = g.Genre.Id,
                    CreatedAt = g.Genre.CreatedAt
                }).ToList()
            }).FirstOrDefault();
        }
    }
}
