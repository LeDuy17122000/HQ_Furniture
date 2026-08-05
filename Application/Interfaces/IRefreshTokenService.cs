using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<LoginResponseDto> GenerateAsync(
            int userId,
            string fullName,
            string email,
            string role);

        Task<TokenResponseDto?> RefreshAsync(string refreshToken);

        Task<bool> RevokeAsync(string refreshToken);
    }
}