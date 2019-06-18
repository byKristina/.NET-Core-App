using System;
using System.Collections.Generic;
using Application.Commands.RolesCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Blog.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGetRolesCommand _getCommand;
        private readonly IGetRoleCommand _getOneCommand;
        private readonly IAddRoleCommand _addCommand;
        private readonly IEditRoleCommand _editCommand;
        private readonly IDeleteRoleCommand _deleteCommand;

        public RolesController(IGetRolesCommand getCommand, IGetRoleCommand getOneCommand, IAddRoleCommand addCommand, IEditRoleCommand editCommand, IDeleteRoleCommand deleteCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }


        // GET: api/Roles
        /// <summary>
        /// Returns all roles (that match provided query).
        /// </summary>
        /// <response code="200">Returns all roles (that match provided query)</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn("Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> Get([FromQuery] RoleSearch search)
        {
            var result = _getCommand.Execute(search);
            return Ok(result);
        }


        // GET: api/Roles/5
        /// <summary>
        /// Gets one role by ID.
        /// </summary>
        /// <response code="200">Gets one role by ID</response>
        /// <response code="404">If role doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn("Admin")]
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


        // POST: api/Roles
        /// <summary>
        /// Creates new role.
        /// </summary>
        /// <response code="201">Adds new role</response>
        /// <response code="409">If role already exist</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn("Admin")]
        [HttpPost]
        public ActionResult Post([FromBody] RoleDto dto)
        {
            try
            {
                _addCommand.Execute(dto);
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

        // PUT: api/Roles/5
        /// <summary>
        /// Edits role.
        /// </summary>
        /// <response code="204">Edits role</response>
        /// <response code="404">If role doesn't exist</response>
        /// <response code="409">If role already exists</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn("Admin")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] RoleDto dto)
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


        // DELETE: api/Roles/5
        /// <summary>
        /// Deletes one role by ID.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="204">Deletes one role by ID</response>
        /// <response code="404">If role doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        /// <response code="401">Unauthorized</response>
        [LoggedIn("Admin")]
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
