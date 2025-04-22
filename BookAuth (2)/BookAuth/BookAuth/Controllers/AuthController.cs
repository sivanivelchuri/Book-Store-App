
using BookAuth.Models;
using BookAuth.Repository.User;
using BookAuth.Service.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Iuser repo;

        private readonly ITokenGenerator service;

        public AuthController(Iuser repo, ITokenGenerator service)
        {
            this.repo = repo;
            this.service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegister user)
        {
            var res = repo.Register(user);
            if (res == 1)
            {
                return Ok("User registered successfully");
            }
            return StatusCode(500, "Something went wrong");
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin user)
        {
            var res = repo.Login(user);
            if (res != null)
            {
                return Ok(service.GenerateToken(user.Email, res.Role));
            }
            return StatusCode(401, "Invalid Email or Password");
        }

    }
}

