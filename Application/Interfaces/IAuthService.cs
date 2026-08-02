using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}