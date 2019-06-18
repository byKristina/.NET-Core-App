using Application.Auth;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Blog.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Encryption _enc;
        private readonly IAuthCommand _authCommand;

        public AuthController(Encryption enc, IAuthCommand authCommand)
        {
            _enc = enc;
            _authCommand = authCommand;
        }



        // POST: api/Auth
        [HttpPost]
        public IActionResult Post(AuthDTO dto)
        {
            
          try
            {
          
            var user = _authCommand.Execute(dto);

            var stringObjekat = JsonConvert.SerializeObject(user);

            var encrypted = _enc.EncryptString(stringObjekat);

            return Ok(new { token = encrypted });
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
          }
        }

        [HttpGet("decode")]
        public IActionResult Decode(string value)
        {
            var decodedString = _enc.DecryptString(value);
            decodedString = decodedString.Replace("\f", "");
            var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);

            return null;
        }
    }
}
