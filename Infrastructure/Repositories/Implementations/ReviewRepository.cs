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

        public async Task<List<Review>> GetAllWithDetailAsync()
        {
            return await context.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<Review?> GetDetailByIdAsync(int id)
        {
            return await context.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.ReviewId == id);
        }

        public async Task<List<Review>> GetByProductAsync(int productId)
        {
            return await context.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<List<Review>> GetByUserAsync(int userId)
        {
            return await context.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            var reviews = await context.Reviews
                .Where(x => x.ProductId == productId)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(x => x.Rating);
        }
    }
}