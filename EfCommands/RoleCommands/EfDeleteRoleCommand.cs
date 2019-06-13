using Application.Commands.RolesCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.RoleCommands
{
    public class EfDeleteRoleCommand : BaseEfCommand, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var role = Context.Roles.Find(request);

            if (role == null || role.IsDeleted == true)
                throw new EntityNotFoundException();

            role.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
