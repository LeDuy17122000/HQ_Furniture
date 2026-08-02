using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await service.RegisterAsync(dto);

            if (!result)
                return BadRequest("Email already exists.");

            return Ok("Register successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await service.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Email or Password is incorrect.");

            return Ok(result);
        }
    }
}