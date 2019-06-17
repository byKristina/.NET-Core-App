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

        public CommentDto Execute(int request)
        {
            var one = Context.Comments.Find(request);

            if (one == null || one.IsDeleted == true)
                throw new EntityNotFoundException("Comment");

            return new CommentDto
            {
                Id = one.Id,
                Text = one.Text,
                PostId = one.PostId,
                UserId = one.UserId,
            };
        }
    }
}
