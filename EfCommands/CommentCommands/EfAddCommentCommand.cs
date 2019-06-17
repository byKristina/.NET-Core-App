using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentCommands
{
    public class EfAddCommentCommand : BaseEfCommand, IAddCommentCommand
    {
        public EfAddCommentCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(CommentDto request)
        {

            if (!Context.Users.Any(r => r.Id == request.UserId))
            {
                throw new EntityNotFoundException("User");
            }

            if (!Context.Posts.Any(r => r.Id == request.PostId))
            {
                throw new EntityNotFoundException("Post");
            }

            Comment comment = new Comment
            {
                Text = request.Text,
                UserId = request.UserId,
                PostId = request.PostId,
            };

            Context.Comments.Add(comment);

            Context.SaveChanges();
        }
    }
}
