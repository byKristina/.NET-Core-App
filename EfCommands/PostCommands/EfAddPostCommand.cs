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
    public class EfAddPostCommand : BaseEfCommand, IAddPostCommand
    {
        public EfAddPostCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(PostDto request)
        {
            if (Context.Posts.Any(p => p.Title == request.Title))
            {
                throw new EntityAlreadyExistsException();
            }


            Post newPost = new Post
            {
               
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
            };

            Context.Posts.Add(newPost);

            Context.SaveChanges();
        }
    }
}
