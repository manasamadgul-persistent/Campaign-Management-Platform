using CMPApiMicroservice.Models;
using CMPApiMicroservice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMPApiMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult getUser()
        {
            try
            {
                var user = _userRepository.getUser();
                return new OkObjectResult(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
            
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult getUserById(int id)
        {
            try
            {
                var user = _userRepository.getUserById(id);
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult addUser([FromBody] User user)
        {
            try
            {
                _userRepository.addUser(user);
                return StatusCode(StatusCodes.Status201Created, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult updateUser([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    _userRepository.updateUser(user);
                }
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteUser(int id)
        {
            try
            {
                _userRepository.deleteUser(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }
    }
}
