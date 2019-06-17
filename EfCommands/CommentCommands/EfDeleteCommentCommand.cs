using Application.Commands.CommentsCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CommentCommands
{
    public class EfDeleteCommentCommand : BaseEfCommand, IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var one = Context.Comments.Find(request);

            if (one == null || one.IsDeleted == true)
            {
                throw new EntityNotFoundException("Comment");
            }

            one.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
