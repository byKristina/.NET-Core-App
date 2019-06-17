using Application.Commands.RolesCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.RoleCommands
{
    public class EfGetRoleCommand : BaseEfCommand, IGetRoleCommand
    {
        public EfGetRoleCommand(BlogContext context) : base(context)
        {
        }

        public RoleDto Execute(int request)
        {
            var role = Context.Roles.Find(request);

            if (role == null || role.IsDeleted)
                throw new EntityNotFoundException("Role");

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
