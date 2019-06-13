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
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search)
        {
            var result =_getCommand.Execute(search);
            return Ok(result);
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "Get")]
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto)
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

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDto dto)
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
