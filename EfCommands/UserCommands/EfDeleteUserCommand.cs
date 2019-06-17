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
            var user = Context.Users.Find(request);

            if (user == null || user.IsDeleted)
                throw new EntityNotFoundException("User");

            user.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
