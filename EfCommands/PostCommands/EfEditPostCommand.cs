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
            var post = Context.Posts.Find(request.Id);

            if (post == null || post.IsDeleted)
                throw new EntityNotFoundException("Post");

            if (!Context.Categories.Any(r => r.Id == request.CategoryId))
            {
                throw new EntityNotFoundException("Category");
            }

            if (!Context.Users.Any(r => r.Id == request.UserId))
            {
                throw new EntityNotFoundException("User");
            }


            post.Title = request.Title;
            post.Content = request.Content;
            post.CategoryId = request.CategoryId;
            post.UserId = request.UserId;
            post.ModifiedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
