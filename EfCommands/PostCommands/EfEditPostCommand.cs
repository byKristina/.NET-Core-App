using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.PostCommands
{
    public class EfEditPostCommand : BaseEfCommand, IEditPostCommand
    {
        public EfEditPostCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(PostDto request)
        {
            var one = Context.Posts.Find(request.Id);

            if (one == null || one.IsDeleted == true)
                throw new EntityNotFoundException("Post");

            if (!Context.Categories.Any(r => r.Id == request.CategoryId))
            {
                throw new EntityNotFoundException("Category");
            }

            if (!Context.Users.Any(r => r.Id == request.UserId))
            {
                throw new EntityNotFoundException("User");
            }


            one.Title = request.Title;
            one.Content = request.Content;
            one.CategoryId = request.CategoryId;
            one.UserId = request.UserId;
            one.ModifiedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
