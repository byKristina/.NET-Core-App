using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Helpers;
using Application.Searches;
using Blog.API.DTO;
using Blog.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IGetPostsCommand _getCommand;
        private readonly IGetPostCommand _getOneCommand;
        private readonly IAddPostCommand _addCommand;
        private readonly IEditPostCommand _editCommand;
        private readonly IDeletePostCommand _deleteCommand;

        public PostsController(IGetPostsCommand getCommand, IGetPostCommand getOneCommand, IAddPostCommand addCommand, IEditPostCommand editCommand, IDeletePostCommand deleteCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }



        // GET: api/Posts
        /// <summary>
        /// Returns all posts (that match provided query).
        /// </summary>
        /// <response code="200">Returns all posts (that match provided query)</response>
        /// <response code="404">If posts don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<IEnumerable<GetPostDto>> Get([FromQuery] PostSearch search)
        {
            try
            {
                var posts = _getCommand.Execute(search);
                return Ok(posts);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error has occurred.");
            }
        }

        // GET: api/Posts/5
        /// <summary>
        /// Gets one post by ID.
        /// </summary>
        /// <response code="200">Gets one post by ID</response>
        /// <response code="404">If post doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var post = _getOneCommand.Execute(id);
                return Ok(post);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error has occurred.");
            }
        }


        // POST: api/Posts
        /// <summary>
        /// Creates new post.
        /// </summary>
        /// <response code="201">Adds new post</response>
        /// <response code="404">If some of the items doesn't exist</response>
        /// <response code="409">If post already exist</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn]
        [HttpPost]
        public ActionResult Post([FromForm] PostImageDto dto)
        {

            var ext = Path.GetExtension(dto.Image.FileName); //.jpg etc.

            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }


            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                var post = new PostDto
                {
                     Id = dto.Id,
                     Title = dto.Title,
                     Content = dto.Content,
                     ImagePath = newFileName,
                     CategoryId = dto.CategoryId,
                     UserId = dto.UserId
                };

                try
                {
                    _addCommand.Execute(post);
                     return StatusCode(201);

                }
                catch (EntityAlreadyExistsException e)
                {
                    return Conflict(e.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Server error has occurred.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error has occurred.");
            }
        }

        // PUT: api/Posts/5
        /// <summary>
        /// Edits post.
        /// </summary>
        /// <response code="204">Edits post</response>
        /// <response code="404">If some of the items doesn't exist</response>
        /// <response code="409">If post already exists</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromForm] PostImageDto dto)
        {
            dto.Id = id;

            var ext = Path.GetExtension(dto.Image.FileName); //.jpg etc.

            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }


            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                var post = new PostDto
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Content = dto.Content,
                    ImagePath = newFileName,
                    CategoryId = dto.CategoryId,
                    UserId = dto.UserId
                };

                try
                { 
                    _editCommand.Execute(post);
                    return NoContent();
                }
   

                catch (EntityNotFoundException e)
                {
                    return NotFound(e.Message);
                }
                catch (EntityAlreadyExistsException e)
                {
                    return Conflict(e.Message);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error has occurred.");
            }
        }

        // DELETE: api/Posts/5
        /// <summary>
        /// Deletes one post by ID.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="204">Deletes one post by ID</response>
        /// <response code="404">If post doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
