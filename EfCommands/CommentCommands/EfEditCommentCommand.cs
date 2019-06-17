using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentCommands
{
    public class EfEditCommentCommand : BaseEfCommand, IEditCommentCommand
    {
        public EfEditCommentCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(CommentDto request)
        {
            var comment = Context.Comments.Find(request.Id);

            if (comment == null || comment.IsDeleted)
                throw new EntityNotFoundException("Comment");

            if (!Context.Users.Any(c => c.Id == request.UserId))
            {
                throw new EntityNotFoundException("User");
            }

            if (!Context.Posts.Any(c => c.Id == request.PostId))
            {
                throw new EntityNotFoundException("Post");
            }

            comment.Text = request.Text;
            comment.UserId = request.UserId;
            comment.PostId = request.PostId;
            comment.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
