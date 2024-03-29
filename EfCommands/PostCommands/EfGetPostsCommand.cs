﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using DataAccess;

namespace EfCommands.PostCommands
{
    public class EfGetPostsCommand : BaseEfCommand, IGetPostsCommand
    {
        public EfGetPostsCommand(BlogContext context) : base(context)
        {
        }

        public PagedResponse<GetPostDto> Execute(PostSearch request)
        {
            var query = Context.Posts.AsQueryable();

            if (request.Title != null)
            {
                query = query.Where(r => r.Title.ToLower().Contains(request.Title.ToLower()));
            }

            if (request.Active.HasValue)
            {
                query = query.Where(c => c.IsDeleted != request.Active);
            }
            else
                query = query.Where(c => c.IsDeleted == false);


            if (request.UserId.HasValue)
            {
                if (!Context.Users.Any(u => u.Id == request.UserId))
                    throw new EntityNotFoundException("User");
                query = query.Where(p => p.UserId == request.UserId);
            }

            if (request.CategoryId.HasValue)
            {
                if (!Context.Categories.Any(c => c.Id == request.CategoryId))
                    throw new EntityNotFoundException("Category");
                query = query.Where(p => p.CategoryId == request.CategoryId);
            }

            var totalCount = query.Count();

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            var response = new PagedResponse<GetPostDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(p => new GetPostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    ImagePath = p.ImagePath,
                    User = p.User.FirstName + " " + p.User.LastName,
                    Category = p.Category.Name,
                    PostedOn = p.CreatedAt

                })
            };

            return response;
        
    }
    }
}
