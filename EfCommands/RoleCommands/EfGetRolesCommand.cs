using Application.Commands.RolesCommands;
using Application.DTO;
using Application.Responses;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.RoleCommands
{
    public class EfGetRolesCommand : BaseEfCommand, IGetRolesCommand
    {
        public EfGetRolesCommand(BlogContext context) : base(context)
        {
        }

        public PagedResponse<RoleDto> Execute(RoleSearch request)
        {
            var query = Context.Roles.AsQueryable();

            if (request.Name != null) {

                query = query.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Active.HasValue)
            {
                query = query.Where(c => c.IsDeleted != request.Active);
            }
            else
                query = query.Where(c => c.IsDeleted == false);

            var totalCount = query.Count();

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            var response = new PagedResponse<RoleDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                })
            };

            return response;

        }
    }
}
