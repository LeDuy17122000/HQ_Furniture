using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext context;

        public RefreshTokenRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await context.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public Task UpdateAsync(RefreshToken refreshToken)
        {
            context.RefreshTokens.Update(refreshToken);

            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}