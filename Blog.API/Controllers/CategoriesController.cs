using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CategoriesCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGetCategoriesCommand _getCommand;
        private readonly IGetCategoryCommand _getOneCommand;
        private readonly IAddCategoryCommand _addCommand;
        private readonly IEditCategoryCommand _editCommand;
        private readonly IDeleteCategoryCommand _deleteCommand;

        public CategoriesController(IGetCategoriesCommand getCommand, IGetCategoryCommand getOneCommand, IAddCategoryCommand addCommand, IEditCategoryCommand editCommand, IDeleteCategoryCommand deleteCommand)
        {
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }



        // GET: api/Categories
        /// <summary>
        /// Returns all categories (that match provided query).
        /// </summary>
        /// <response code="200">Returns all categories (that match provided query)</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get([FromQuery] CategorySearch search)
        {
            var categories =_getCommand.Execute(search);
            return Ok(categories);
        }


        // GET: api/Categories/5
        /// <summary>
        /// Gets one category by ID.
        /// </summary>
        /// <response code="200">Gets one category by ID</response>
        /// <response code="404">If category doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var category = _getOneCommand.Execute(id);
                return Ok(category);
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

        // POST: api/Categories
        /// <summary>
        /// Creates new category.
        /// </summary>
        /// <response code="201">Adds new category</response>
        /// <response code="409">If category already exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromBody] CategoryDto dto)
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

        // PUT: api/Categories/5
        /// <summary>
        /// Edits category.
        /// </summary>
        /// <response code="204">Edits category</response>
        /// <response code="404">If category doesn't exist</response>
        /// <response code="409">If category already exists</response>
        /// <response code="500">If server error occurred</response>
         [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryDto dto)
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

        // DELETE: api/Categories/5
        /// <summary>
        /// Deletes one category by ID.
        /// </summary>
        /// <param name="id"></param>        
        /// <response code="204">Deletes one category by ID</response>
        /// <response code="404">If category doesn't exist</response>
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
