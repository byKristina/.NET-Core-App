using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PostsCommands
{
    public interface IDeletePostCommand : ICommand<int>
    {
    }
}
