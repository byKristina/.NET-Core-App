using Application.Auth;
using Blog.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Encryption _enc;

        public AuthController(Encryption enc)
        {
            _enc = enc;
        }

        // POST: api/Auth
        [HttpPost]
        public IActionResult Post()
        {
            // TODO : get user from database 
           
            var user = new LoggedUser
            {
                FirstName = "John",
                LastName = "Doe",
                Id = 1,
                Role = "Admin",
                Username = "pass123"
            };

            var stringObjekat = JsonConvert.SerializeObject(user);

            var encrypted = _enc.EncryptString(stringObjekat);

            return Ok(new { token = encrypted });
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
