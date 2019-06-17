using Application.Commands.CategoriesCommands;
using Application.DTO;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CategoryCommands
{
    public class EfEditCategoryCommand : BaseEfCommand, IEditCategoryCommand
    {
        public EfEditCategoryCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(CategoryDto request)
        {
            var category = Context.Categories.Find(request.Id);

            if (category == null || category.IsDeleted == true)
                throw new EntityNotFoundException("Category");

            if (category.Name == request.Name)
            {
                throw new EntityAlreadyExistsException("Name");
            }
           
            category.Name = request.Name;
            category.ModifiedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
