using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
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


            if (request.UserId.HasValue)
            {
                if (!Context.Users.Any(u => u.Id == request.UserId))
                    throw new EntityNotFoundException("User");
                query = query.Where(c => c.UserId == request.UserId);
            }

            if (request.PostId.HasValue)
            {
                if (!Context.Posts.Any(p => p.Id == request.PostId))
                    throw new EntityNotFoundException("Post");
                query = query.Where(c => c.PostId == request.PostId);
            }


            return query.Select(c => new GetCommentDto
                {
                    Id = c.Id,
                    Comment = c.Text,
                    User = c.User.Username,
                    Post = c.Post.Title
            });

        
        }
    }
}
