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
            var one = Context.Roles.Find(request);

            if (one == null || one.IsDeleted == true)
                throw new EntityNotFoundException("Role");

            return new RoleDto
            {
                Id = one.Id,
                Name = one.Name
            };
        }
    }
}
