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
            var one = Context.Comments.Find(request.Id);

            if (one == null || one.IsDeleted == true)
                throw new EntityNotFoundException();


            one.Text = request.Text;
            one.UserId = request.UserId;
            one.PostId = request.PostId;
            one.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
