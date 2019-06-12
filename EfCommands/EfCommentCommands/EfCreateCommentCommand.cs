using Application.DTO;
using Application.Exceptions;
using Application.ICommands.ICommentCommands;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfCommentCommands
{
    public class EfCreateCommentCommand : BaseEfCommand, ICreateCommentComand
    {
        public EfCreateCommentCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(CreateCommentDto request)
        {
            var user = Context.Users.Find(request.UserId);
            if (user == null || user.IsDeleted) throw new EntityNotFoundException();
            var movie = Context.Movies.Find(request.MovieId);
            if (movie == null || movie.IsDeleted) throw new EntityNotFoundException();
            Context.Comments.Add(new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Text = request.Text,
                Movie = movie,
                User = user
            });
            Context.SaveChanges();
        }
    }
}
