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

        public void Execute(int request)
        {
            var one = Context.Users.Find(request);

            if (one == null || one.IsDeleted == true)
                throw new EntityNotFoundException();

            one.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
