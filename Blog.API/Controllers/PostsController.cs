using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PostsCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult Get([FromQuery] PostSearch search)
        {
            var result = _getCommand.Execute(search);
            return Ok(result);
        }

        // GET: api/Posts/5
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

        // POST: api/Posts
        [HttpPost]
        public IActionResult Post([FromBody] PostDto dto)
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

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostDto dto)
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
