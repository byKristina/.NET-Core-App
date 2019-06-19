using DataAccess;
using Domain;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BlogContext();

            context.Roles.Add(new Role
            {
               Name = "Admin"
            });
   
            context.Roles.Add(new Role
            {
                Name = "User"
            });

            // admin
            context.Users.Add(new User
            {
              FirstName = "John",
              LastName = "Doe",
              Username = "admin",
              Password = "pass123",
              Email = "admin@email.com",
              RoleId = 1
            });

            // user
            context.Users.Add(new User
            {
                FirstName = "Jane",
                LastName = "Doe",
                Username = "user",
                Password = "pass123",
                Email = "user@email.com",
                RoleId = 2
            });


            context.SaveChanges();
        }
    }
}
