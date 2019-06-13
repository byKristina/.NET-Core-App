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
    public class EfAddCommentCommand : BaseEfCommand, IAddCommentCommand
    {
        public EfAddCommentCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(CommentDto request)
        {
           
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
