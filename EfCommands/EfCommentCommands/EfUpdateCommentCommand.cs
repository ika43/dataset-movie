using Application.DTO;
using Application.Exceptions;
using Application.ICommands.ICommentCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfCommentCommands
{
    public class EfUpdateCommentCommand : BaseEfCommand, IUpdateCommentComand
    {
        public EfUpdateCommentCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(UpdateCommentDto request)
        {
            var comment = Context.Comments.Find(request.Id);
            if (comment == null || comment.IsDeleted) throw new EntityNotFoundException();
            comment.Text = request.Text;
            Context.SaveChanges();
        }
    }
}
