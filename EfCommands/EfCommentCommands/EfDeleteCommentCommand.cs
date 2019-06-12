using Application.Exceptions;
using Application.ICommands.ICommentCommands;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.EfCommentCommands
{
    public class EfDeleteCommentCommand : BaseEfCommand, IDeleteCommentComand
    {
        public EfDeleteCommentCommand(MovieContext ctx) : base(ctx)
        {

        }

        public void Execute(string request)
        {
            var comment = Context.Comments.Find(request);
            if (comment == null || comment.IsDeleted) throw new EntityNotFoundException();
            comment.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
