using Application.Commands.UsersCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
    public class EfAddUserCommand : BaseEfCommand, IAddUserCommand
    {

        private readonly IEmailSender _emailSender;

        public EfAddUserCommand(BlogContext context, IEmailSender _emailSender) : base(context)
        {
            this._emailSender = _emailSender;
        }

        public void Execute(UserDto request)
        {
            if (Context.Users.Any(u => u.Username == request.Username))
            {
                throw new EntityAlreadyExistsException("Username");
            }
            if (Context.Users.Any(u => u.Email == request.Email))
            {
                throw new EntityAlreadyExistsException("Email");
            }

            if (!Context.Roles.Any(r => r.Id == request.RoleId))
            {
                throw new EntityNotFoundException("Role");
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

            _emailSender.Subject = "Registration";
            _emailSender.ToEmail = request.Email;
            _emailSender.Body = "You have successfully registered!";
            _emailSender.Send();


        }
    }
}
