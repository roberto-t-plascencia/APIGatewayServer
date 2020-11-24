using Microservice.gateway.api.Model;
using Microservice.gateway.api.Repository;
using Microservice.gateway.api.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Microservice.gateway.api.Controllers
{
    //[Authorize] //for Active Directory integration
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserRepository _userRepository;

        public LoginController(IUserRepository repo)
        {
            _userRepository = repo;
        }

        //[Authorize] //for Active Directory integration
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody]Register user)
        {
            string returnValue = await _userRepository.Add(user);
            if (string.IsNullOrEmpty(returnValue))
                return StatusCode(403);
            else
                return Ok(200);
        }

        //[Authorize] //for AD integration
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> UserLogin(Login login)
        {
            Register user = await _userRepository.Login(login);
            if (user == null)
                return StatusCode(403);
            else
                return Ok(200);
        }

        // [Authorize] //for AD integration
        [Route("Deactivate/{username}")]
        [HttpPost]
        public async Task<ActionResult> Remove(string username)
        {
            var returnValue = await _userRepository.Remove(username);
            if (string.IsNullOrEmpty(returnValue))
                return StatusCode(404);
            else
                return Ok(200);
        }

        //[Authorize] //for AD integration
        [Route("GetUsers")]
        [HttpGet]
        public IEnumerable<Register> GetAll()
        {
            return _userRepository.GetUsers();
        }

    }
}
