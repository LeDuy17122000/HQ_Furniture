using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class ReviewRepository
        : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<List<Review>> GetByProductAsync(int productId)
        {
            return await dbSet
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<List<Review>> GetByUserAsync(int userId)
        {
            return await dbSet
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}