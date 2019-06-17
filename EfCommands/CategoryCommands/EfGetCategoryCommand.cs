using Application.Commands.CategoriesCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CategoryCommands
{
    public class EfGetCategoryCommand : BaseEfCommand, IGetCategoryCommand
    {
        public EfGetCategoryCommand(BlogContext context) : base(context)
        {
        }

        public CategoryDto Execute(int request)
        {
            var category = Context.Categories.Find(request);

            if (category == null || category.IsDeleted == true)
                throw new EntityNotFoundException("Category");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
