using Application.Commands.RolesCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.RoleCommands
{
    public class EfAddRoleCommand : BaseEfCommand, IAddRoleCommand
    {
        public EfAddRoleCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(RoleDto request)
        {
            if (Context.Roles.Any(r => r.Name == request.Name)) {
                throw new EntityAlreadyExistsException("Role");
            }

            Role newRole = new Role
            {
                Name = request.Name
            };

            Context.Roles.Add(newRole);

            Context.SaveChanges();
        }
    }
}
