using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Application.Auth;

namespace Blog.API.Helpers
{
    public class LoggedIn : Attribute, IResourceFilter
    {
        private readonly string _role;

        public LoggedIn(string role)
        {
            _role = role;
        }

        public LoggedIn()
        {

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var user = context.HttpContext.RequestServices.GetService<LoggedUser>();

            if (!user.IsLogged)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                if (_role != null)
                {
                    if (user.Role != _role)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
