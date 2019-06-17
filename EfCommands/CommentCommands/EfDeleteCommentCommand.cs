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

        public void Execute(int id)
        {
            var comment = Context.Comments.Find(id);

            if (comment == null || comment.IsDeleted)
            {
                throw new EntityNotFoundException("Comment");
            }

            comment.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
