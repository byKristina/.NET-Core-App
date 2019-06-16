using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CategoriesCommands;
using Application.Commands.CommentsCommands;
using Application.Commands.PostsCommands;
using Application.Commands.RolesCommands;
using Application.Commands.UsersCommands;
using Application.Interfaces;
using Blog.API.Email;
using DataAccess;
using EfCommands.CategoryCommands;
using EfCommands.CommentCommands;
using EfCommands.PostCommands;
using EfCommands.RoleCommands;
using EfCommands.UserCommands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<BlogContext>();

            services.AddTransient<IGetCategoriesCommand, EfGetCategoriesCommand>();
            services.AddTransient<IGetCategoryCommand, EfGetCategoryCommand>();
            services.AddTransient<IAddCategoryCommand, EfAddCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();

            services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
            services.AddTransient<IGetRoleCommand, EfGetRoleCommand>();
            services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();

           
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            services.AddTransient<IAddUserCommand, EfAddUserCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();


            services.AddTransient<IGetPostCommand, EfGetPostCommand>();
            services.AddTransient<IGetPostsCommand, EfGetPostsCommand>();
            services.AddTransient<IAddPostCommand, EfAddPostCommand>();
            services.AddTransient<IEditPostCommand, EfEditPostCommand>();
            services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();

            services.AddTransient<IGetCommentCommand, EfGetCommentCommand>();
            services.AddTransient<IGetCommentsCommand, EfGetCommentsCommand>();
            services.AddTransient<IAddCommentCommand, EfAddCommentCommand>();
            services.AddTransient<IEditCommentCommand, EfEditCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();

            var section = Configuration.GetSection("Email");

            var sender =
                new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
