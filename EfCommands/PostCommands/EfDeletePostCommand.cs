using Application.Commands.PostsCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.PostCommands
{
    public class EfDeletePostCommand : BaseEfCommand, IDeletePostCommand
    {
        public EfDeletePostCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var post = Context.Posts.Find(id);

            if (post == null || post.IsDeleted)
            {
                throw new EntityNotFoundException("Post");
            }

            post.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
