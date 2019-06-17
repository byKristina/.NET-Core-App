using Application.Commands.CategoriesCommands;
using Application.DTO;
using Application.Responses;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CategoryCommands
{
    public class EfGetCategoriesCommand : BaseEfCommand, IGetCategoriesCommand
    {
        public EfGetCategoriesCommand(BlogContext context) : base(context)
        {
        }

        public PagedResponse<CategoryDto> Execute(CategorySearch request)
        {
            var query = Context.Categories.AsQueryable();

            if (request.Name != null)
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()) );
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


            var response = new PagedResponse<CategoryDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                })
            };

            return response;

        }
    }
}
