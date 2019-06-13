using DataAccess;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BlogContext();

            context.Categories.Add(new Category
                {
                    Name = "Category 1",
                   
                });

            context.SaveChanges();
        }
    }
}
