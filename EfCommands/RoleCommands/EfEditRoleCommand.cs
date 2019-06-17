using Application.Commands.RolesCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.RoleCommands
{
    public class EfEditRoleCommand : BaseEfCommand, IEditRoleCommand
    {
        public EfEditRoleCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(RoleDto request)
        {
            var role = Context.Roles.Find(request.Id);

            if (role == null || role.IsDeleted)
            {
                throw new EntityNotFoundException("Role");
            }

            if (role.Name == request.Name)
            {
                throw new EntityAlreadyExistsException("Name");
            }

                role.Name = request.Name;
                role.ModifiedAt = DateTime.Now;
                Context.SaveChanges();

        }
    }
}
