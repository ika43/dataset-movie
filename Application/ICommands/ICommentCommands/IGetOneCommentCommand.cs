using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ICommands.ICommentCommands
{
    public interface IGetOneCommentCommand : ICommand<string, CommentDto>
    {
    }
}
