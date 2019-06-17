using Application.Commands.UsersCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
    public class EfEditUserCommand : BaseEfCommand, IEditUserCommand
    {
        public EfEditUserCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(UserDto request)
        {
            var user = Context.Users.Find(request.Id);

            if (user == null || user.IsDeleted)
                throw new EntityNotFoundException("User");

            if (user.Email != request.Email)
            {
                   if (Context.Users.Any(u => u.Email == request.Email))
                {
                    throw new EntityAlreadyExistsException("Email");
                }
            }
            if (user.Username != request.Username)
            {
                   if (Context.Users.Any(u => u.Username == request.Username))
               {
                    throw new EntityAlreadyExistsException("Username");
               }
            }

            if (!Context.Roles.Any(r => r.Id == request.RoleId))
            {
                throw new EntityNotFoundException("Role");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = request.Password;
            user.RoleId = request.RoleId;
            user.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
