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
            var one = Context.Roles.Find(request.Id);

            if (one == null || one.IsDeleted == true)
            {
                throw new EntityNotFoundException("Role");
            }

            if (one.Name == request.Name)
            {
                throw new EntityAlreadyExistsException("Name");
            }

                one.Name = request.Name;
                one.ModifiedAt = DateTime.Now;
                Context.SaveChanges();

        }
    }
}
