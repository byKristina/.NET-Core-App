using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Responses;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentCommands
{

    public class EfGetCommentsCommand : BaseEfCommand, IGetCommentsCommand
    {
        public EfGetCommentsCommand(BlogContext context) : base(context)
        {
        }

        public PagedResponse<GetCommentDto> Execute(CommentSearch request)
        {
            var query = Context.Comments.AsQueryable();

            if (request.Active.HasValue)
            {
                query = query.Where(c => c.IsDeleted != request.Active);
            }
            else
                query = query.Where(c => c.IsDeleted == false);


            if (request.UserId != null)
            {
                query = query.Where(r => r.UserId == request.UserId);
            }

            if (request.PostId != null)
            {
                query = query.Where(r => r.PostId == request.PostId);
            }

            var totalCount = query.Count();

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            var response = new PagedResponse<GetCommentDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(c => new GetCommentDto
                {
                    Id = c.Id,
                    Comment = c.Text,
                    User = c.User.Username,
                    Post = c.Post.Title,
                  
                })
            };

            return response;
        }
    }
}
