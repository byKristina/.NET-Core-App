using System;
using System.Collections.Generic;
using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGetCommentsCommand _getCommand;
        private readonly IGetCommentCommand _getOneCommand;
        private readonly IAddCommentCommand _addCommand;
        private readonly IEditCommentCommand _editCommand;
        private readonly IDeleteCommentCommand _deleteCommand;

        public CommentsController(IGetCommentsCommand getCommand, IGetCommentCommand getOneCommand, IAddCommentCommand addCommand, IEditCommentCommand editCommand, IDeleteCommentCommand deleteCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }



        // GET: api/Comments
        /// <summary>
        /// Returns all comments (that match provided query).
        /// </summary>
        /// <response code="200">Returns all comments (that match provided query)</response>
        /// <response code="404">If comments don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<IEnumerable<GetCommentDto>> Get([FromQuery] CommentSearch search)
        {
            try
            {
                var comments = _getCommand.Execute(search);
                return Ok(comments);
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


        // GET: api/Comments/5
        /// <summary>
        /// Gets one comment by ID.
        /// </summary>
        /// <response code="200">Gets one comment by ID</response>
        /// <response code="404">If comment doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var one = _getOneCommand.Execute(id);
                return Ok(one);
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


        // POST: api/Comments
        /// <summary>
        /// Creates new comment.
        /// </summary>
        /// <response code="201">Adds new comment</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>

        [HttpPost]
        public ActionResult Post([FromBody] CommentDto dto)
        {
            try
            {
                _addCommand.Execute(dto);
                return StatusCode(201);
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

        // PUT: api/Comments/5
        /// <summary>
        /// Edits comment.
        /// </summary>
        /// <response code="204">Edits comment</response>
        /// <response code="404">If some of the items doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CommentDto dto)
        {
            dto.Id = id;

            try
            {
                _editCommand.Execute(dto);
                return NoContent();
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


        // DELETE: api/Comments/5
        /// <summary>
        /// Deletes one comment by ID.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="204">Deletes one comment by ID</response>
        /// <response code="404">If comment doesn't exist</response>
        /// <response code="500">If server error occurred</response>
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
                return StatusCode(500, "Server error has occurred.");
            }
        }
    }
}
