using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IRefreshTokenService refreshTokenService;

        public AuthController(
            IAuthService authService,
            IRefreshTokenService refreshTokenService)
        {
            this.authService = authService;
            this.refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await authService.RegisterAsync(dto);

            if (!result)
                return BadRequest("Email already exists.");

            return Ok("Register successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Email or Password is incorrect.");

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenRequestDto dto)
        {
            var result = await refreshTokenService.RefreshAsync(dto.RefreshToken);

            if (result == null)
                return Unauthorized("Invalid Refresh Token.");

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(TokenRequestDto dto)
        {
            var result = await refreshTokenService.RevokeAsync(dto.RefreshToken);

            if (!result)
                return BadRequest("Logout failed.");

            return Ok("Logout successfully.");
        }
    }
}