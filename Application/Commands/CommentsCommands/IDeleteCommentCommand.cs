using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.CommentsCommands
{
    public interface IDeleteCommentCommand : ICommand<int>
    {
    }
}
