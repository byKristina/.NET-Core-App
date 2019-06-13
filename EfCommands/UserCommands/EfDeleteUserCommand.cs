using Application.Commands.UsersCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.UserCommands
{
    public class EfDeleteUserCommand : BaseEfCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(BlogContext context) : base(context)
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
