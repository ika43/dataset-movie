using Application.DTO;
using Application.ICommands.IMovieCommands;
using Application.SearchObj;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfMovieCommands
{
    public class EfGetMovieCommand : BaseEfCommand, IGetMovieCommand
    {
        public EfGetMovieCommand(MovieContext ctx) : base(ctx)
        {

        }

        public IEnumerable<MovieDto> Execute(MovieSearch request)
        {
            var query = Context.Movies.AsQueryable();
            query = query.Where(p => !p.IsDeleted);
            if(request.Title != null)
            {
                query = query.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
            }
            if (request.MaxRunTime.HasValue)
            {
                query = query.Where(p => p.Runtime <= request.MaxRunTime);
            }
            if (request.MinRuntime.HasValue)
            {
                query = query.Where(p => p.Runtime >= request.MinRuntime);
            }
            if (request.MinImdbRating.HasValue)
            {
                query = query.Where(p => p.ImdbRating >= request.MinImdbRating);
            }
            if (request.MaxImdbRating.HasValue)
            {
                query = query.Where(p => p.ImdbRating <= request.MaxImdbRating);
            }
            return query.Select(p => new MovieDto
            {
                Id = p.Id,
                Title = p.Title,
                Plot = p.Plot,
                Released = p.Released,
                Runtime = p.Runtime,
                CreatedAt = p.CreatedAt,
                ImdbRating = p.ImdbRating,
                Comments = p.Comments.Select(c=> new CommentDto
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
            });
        }
    }
}
