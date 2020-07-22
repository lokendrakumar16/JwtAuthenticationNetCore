using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auth.Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public UserController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user.UserName, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
