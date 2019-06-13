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
            var one = Context.Users.Find(request.Id);

            if (one == null || one.IsDeleted == true)
                throw new EntityNotFoundException();

            if (one.Email != request.Email)
            {
                   if (Context.Users.Any(u => u.Email == request.Email))
                {
                    throw new EntityAlreadyExistsException();
                }
            }
            if (one.Username != request.Username)
            {
                   if (Context.Users.Any(u => u.Username == request.Username))
               {
                    throw new EntityAlreadyExistsException();
               }
            }

            one.FirstName = request.FirstName;
            one.LastName = request.LastName;
            one.Username = request.Username;
            one.Email = request.Email;
            one.Password = request.Password;
            one.RoleId = request.RoleId;
            one.ModifiedAt = DateTime.Now;

            Context.SaveChanges();

        }
    }
}
