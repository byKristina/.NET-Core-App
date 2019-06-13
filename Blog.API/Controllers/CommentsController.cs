using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CommentsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult Get([FromQuery] CommentSearch search)
        {
            var result = _getCommand.Execute(search);
            return Ok(result);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var one = _getOneCommand.Execute(id);
                return Ok(one);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto)
        {
            try
            {
                _addCommand.Execute(dto);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistsException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CommentDto dto)
        {
            try
            {
                _editCommand.Execute(dto);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (EntityAlreadyExistsException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
