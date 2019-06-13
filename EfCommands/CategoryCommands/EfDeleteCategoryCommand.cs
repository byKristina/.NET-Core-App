using Application.Commands.CategoriesCommands;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CategoryCommands
{
    public class EfDeleteCategoryCommand : BaseEfCommand, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(BlogContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var category = Context.Categories.Find(request);

            if (category == null || category.IsDeleted == true)
                throw new EntityNotFoundException();

            category.IsDeleted = true;
           
            Context.SaveChanges();

        }
    }
}
