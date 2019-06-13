using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.PostCommands
{
    public class EfGetPostCommand : BaseEfCommand, IGetPostCommand
    {
        public EfGetPostCommand(BlogContext context) : base(context)
        {
        }

        public PostDto Execute(int request)
        {
            var post = Context.Posts.Find(request);

            if (post == null || post.IsDeleted == true)
                throw new EntityNotFoundException();

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                CategoryId = post.CategoryId
           
        };
        }
    }
}
