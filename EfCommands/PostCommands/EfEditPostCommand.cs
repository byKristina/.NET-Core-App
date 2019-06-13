using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
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
                throw new EntityNotFoundException();

            
            one.Title = request.Title;
            one.Content = request.Content;
            one.CategoryId = request.CategoryId;
            one.UserId = request.UserId;
            one.ModifiedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
