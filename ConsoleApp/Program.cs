using DataAccess;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BlogContext();

            //context.Categories.Add(new Category
            //    {
            //        Name = "Category 1",

            //    });

            context.Users.Add(new User
            {
              FirstName = "Novi",
              LastName = "Doe",
              Username = "johndoe3",
              Password = "pass123",
              Email = "mail@email.com",
              RoleId = 1
            });

            context.Posts.Add(new Post
            {

                Title = "Title 1",
                Content = "Content",
                UserId = 1,
                CategoryId = 1,
            });

         

            context.SaveChanges();
        }
    }
}
