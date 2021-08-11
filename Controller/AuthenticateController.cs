using CMPApiMicroservice.Models;
using CMPApiMicroservice.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult authenticateUser([FromBody]UserLogin userLogin)
        {
            var user = _authenticateService.Authenticate(userLogin.UserName, userLogin.Password);
            if(user == null)
            {
                return BadRequest(new { message = "UserName or Password is invalid" });
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
