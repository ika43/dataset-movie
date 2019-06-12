using Application.DTO;
using Application.ICommands.IGenreCommands;
using Application.SearchObj;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfGenreCommands
{
    public class EfGetGenreCommand : BaseEfCommand, IGetGenreCommand
    {
        public EfGetGenreCommand(MovieContext ctx) : base(ctx) { }

        public IEnumerable<GenreDto> Execute(GenreSearch request)
        {
            var query = Context.Genres.AsQueryable();

            if (request.Name != null)
            {
                query = query.Where(g => g.Name.ToLower().Trim().Contains(request.Name.ToLower()));
            }

            if(request.IsDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == request.IsDeleted);
            } else
            {
                query = query.Where(p => p.IsDeleted == false);
            }

            return query.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name,
                CreatedAt = g.CreatedAt
            });
        }
    }
}
