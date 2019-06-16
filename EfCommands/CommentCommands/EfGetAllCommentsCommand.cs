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

    public class EfGetAllCommentsCommand : BaseEfCommand, IGetAllCommentsCommand
    {
        public EfGetAllCommentsCommand(BlogContext context) : base(context)
        {
        }

        public IEnumerable<GetCommentDto> Execute(CommentSearch request)
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

        
             return  query.Select(c => new GetCommentDto
                {
                    Id = c.Id,
                    Comment = c.Text,
                    User = c.User.Username,
                    Post = c.Post.Title
            });

        
        }
    }
}
