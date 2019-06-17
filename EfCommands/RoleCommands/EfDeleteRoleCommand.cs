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

        public void Execute(int id)
        {
            var role = Context.Roles.Find(id);

            if (role == null || role.IsDeleted)
                throw new EntityNotFoundException("Role");

            role.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
