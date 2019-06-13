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
    public class EfAddUserCommand : BaseEfCommand, IAddUserCommand
    {
        public EfAddUserCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(UserDto request)
        {
            if (Context.Users.Any(u => u.Username == request.Username))
            {
                throw new EntityAlreadyExistsException();
            }
            if (Context.Users.Any(u => u.Email == request.Email))
            {
                throw new EntityAlreadyExistsException();
            }

            User user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                RoleId = request.RoleId,
            };

            Context.Users.Add(user);

            Context.SaveChanges();
        }
    }
}
