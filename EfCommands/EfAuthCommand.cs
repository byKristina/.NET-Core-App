using Application.Auth;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAuthCommand : BaseEfCommand, IAuthCommand
    {
        public EfAuthCommand(BlogContext context) : base(context)
        {
        }

        public LoggedUser Execute(AuthDTO request)
        {
            var user = Context.Users
                .Include(u => u.Role)
                .Where(u => u.Username == request.Username)
                .Where(u => u.Password == request.Password)
                .FirstOrDefault();


            if (user == null)
                throw new EntityNotFoundException("User");

            return new LoggedUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                IsLogged = true,
                
                Role = user.Role.Name,
                Username = user.Username
            };
        }
    }
}
