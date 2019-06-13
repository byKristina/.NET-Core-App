using Application.Commands.CategoriesCommands;
using Application.DTO;
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

        public IEnumerable<CategoryDto> Execute(CategorySearch request)
        {
            var query = Context.Categories.AsQueryable();

            if (request.Keyword != null)
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.Keyword.ToLower()) );
            }

            if (request.Active.HasValue)
            {
                query = query.Where(c => c.IsDeleted != request.Active);
            }
            else
                query = query.Where(c => c.IsDeleted == false);

            return query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
