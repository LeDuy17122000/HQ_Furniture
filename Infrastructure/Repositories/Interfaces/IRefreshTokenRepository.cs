using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);

        Task<RefreshToken?> GetByTokenAsync(string token);

        Task UpdateAsync(RefreshToken refreshToken);

        Task SaveAsync();
    }
}