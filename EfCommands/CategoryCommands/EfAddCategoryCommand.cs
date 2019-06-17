using Application.Commands.CategoriesCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CategoryCommands
{
    public class EfAddCategoryCommand : BaseEfCommand, IAddCategoryCommand
    {
        public EfAddCategoryCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(CategoryDto request)
        {
            if (Context.Categories.Any(c => c.Name == request.Name)) {
                throw new EntityAlreadyExistsException("Category");
            }

            Category newCategory = new Category
            {
                Name = request.Name.Trim(),
                
            };
            Context.Categories.Add(newCategory);

            Context.SaveChanges();
        }
    }
}
