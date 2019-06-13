using Application.Commands.UsersCommands;
using Application.DTO;
using Application.Responses;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
    public class EfGetUsersCommand : BaseEfCommand, IGetUsersCommand
    {
        public EfGetUsersCommand(BlogContext context) : base(context)
        {
        }

        public PagedResponse<UserDto> Execute(UserSearch request)
        {
            var query = Context.Users.AsQueryable();

            if (request.FirstName != null)
            {
                query = query.Where(r => r.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            }
            if (request.LastName != null)
            {
                query = query.Where(r => r.LastName.ToLower().Contains(request.LastName.ToLower()));
            }
            if (request.Username != null)
            {
                query = query.Where(r => r.Username.ToLower().Contains(request.Username.ToLower()));
            }

            if (request.Active.HasValue)
            {
                query = query.Where(c => c.IsDeleted != request.Active);
            }
            else
                query = query.Where(c => c.IsDeleted == false);

            
            if (request.RoleId != null)
            {
                query = query.Where(r => r.RoleId == request.RoleId);
            }

            var totalCount = query.Count();

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            var response = new PagedResponse<UserDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.Username,
                    Password = u.Password,
                    RoleId = u.RoleId,
                  
                })
            };

            return response;
        }
    }
}
