using Application.DTO;
using Application.ICommands.ICommentCommands;
using Application.SearchObj;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCommentCommands
{
    public class EfGetCommentCommand : BaseEfCommand, IGetCommentComand
    {
        public EfGetCommentCommand(MovieContext ctx) : base(ctx)
        {

        }

        public IEnumerable<CommentDto> Execute(CommentSearch request)
        {
            var query = Context.Comments.AsQueryable();
            if (request.IsDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == request.IsDeleted);
            }
            else
            {
                query = query.Where(p => !p.IsDeleted);
            }
            if(request.Email != null)
            {
                query = query.Where(p => p.User.Email.ToLower().Contains(request.Email.ToLower()));
            }
            if(request.Movie != null)
            {
                query = query.Where(p => p.Movie.Title.ToLower().Contains(request.Movie.ToLower()));
            }
            var TotalRecords = query.Count();
            if (request.page.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.page ?? 0 - 1) * request.PageSize ?? 0).Take(request.PageSize ?? 0);
            }
            return query.Select(p => new CommentDto
            {
                Id = p.Id,
                Text = p.Text,
                User = p.User.Email,
                Movie = p.Movie.Title,
                CreatedAt = p.CreatedAt,
                TotalRecords = TotalRecords
            });
        }
    }
}
