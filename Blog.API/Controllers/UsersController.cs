using System;
using System.Collections.Generic;
using Application.Commands.UsersCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGetUsersCommand _getCommand;
        private readonly IGetUserCommand _getOneCommand;
        private readonly IAddUserCommand _addCommand;
        private readonly IEditUserCommand _editCommand;
        private readonly IDeleteUserCommand _deleteCommand;

        public UsersController(IGetUsersCommand getCommand, IGetUserCommand getOneCommand, IAddUserCommand addCommand, IEditUserCommand editCommand, IDeleteUserCommand deleteCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }

        // GET: api/Users
        /// <summary>
        /// Returns all users (that match provided query).
        /// </summary>
        /// <response code="200">Returns all users (that match provided query)</response>
        /// <response code="404">If users don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<IEnumerable<GetUserDto>> Get([FromQuery] UserSearch search)
        {
            try
            {
                var users = _getCommand.Execute(search);
                return Ok(users);
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


        // GET: api/Users/5
        /// <summary>
        /// Gets one user by ID.
        /// </summary>
        /// <response code="200">Gets one user by ID</response>
        /// <response code="404">If user doesn't exist</response>
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

        // POST: api/Users
        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <response code="201">Adds new user</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If user already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromBody] UserDto dto)
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
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error has occurred.");
            }
        }

        // PUT: api/Users/5
        /// <summary>
        /// Edits user.
        /// </summary>
        /// <response code="204">Edits user</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If user already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]  UserDto dto)
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
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error has occurred.");
            }
        }


        // DELETE: api/Users/5
        /// <summary>
        /// Deletes one user by ID.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="204">Deletes one user by ID</response>
        /// <response code="404">If user doesn't exist</response>
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
