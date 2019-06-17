using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CommentCommands
{
    public class EfGetCommentCommand : BaseEfCommand, IGetCommentCommand
    {
        public EfGetCommentCommand(BlogContext context) : base(context)
        {
        }

        public CommentDto Execute(int id)
        {
            var comment = Context.Comments.Find(id);

            if (comment == null || comment.IsDeleted)
                throw new EntityNotFoundException("Comment");

            return new CommentDto
            {
                Id = comment.Id,
                Text = comment.Text,
                PostId = comment.PostId,
                UserId = comment.UserId,
            };
        }
    }
}
