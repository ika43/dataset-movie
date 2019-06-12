using Application.DTO;
using Application.Interfaces;
using Application.SearchObj;
using System.Collections.Generic;

namespace Application.ICommands.ICommentCommands
{
    public interface IGetCommentComand : ICommand<CommentSearch, IEnumerable<CommentDto>>
    {
    }
}

