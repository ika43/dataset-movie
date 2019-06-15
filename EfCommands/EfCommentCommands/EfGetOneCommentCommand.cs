using Application.DTO;
using Application.Exceptions;
using Application.ICommands.ICommentCommands;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfCommentCommands
{
    public class EfGetOneCommentCommand : BaseEfCommand, IGetOneCommentCommand
    {
        public EfGetOneCommentCommand(MovieContext ctx) : base(ctx)
        {

        }

        public CommentDto Execute(string request)
        {
            var query = Context.Comments.AsQueryable();
            query = query.Where(p => p.Id == request);
            if (!query.Any() || query.Any(p => p.IsDeleted)) throw new EntityNotFoundException();
            return
                query.Select(p => new CommentDto
                {
                    Id = p.Id,
                    Text = p.Text,
                    User = p.User.Email,
                    Movie = p.Movie.Title,
                    CreatedAt = p.CreatedAt
                }).FirstOrDefault();
        }
    }
}
