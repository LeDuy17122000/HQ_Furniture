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
                .Where(x => x.ProductId == productId && x.IsApproved)
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
                .Where(x => x.ProductId == productId && x.IsApproved)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(x => x.Rating);
        }

        // ==========================
        // Review Authorization
        // ==========================

        public async Task<List<Review>> GetPendingAsync()
        {
            return await context.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => !x.IsApproved)
                .ToListAsync();
        }

        public async Task ApproveAsync(int reviewId)
        {
            var review = await context.Reviews.FindAsync(reviewId);

            if (review == null)
                throw new Exception("Review not found.");

            review.IsApproved = true;

            await context.SaveChangesAsync();
        }

        public async Task RejectAsync(int reviewId)
        {
            var review = await context.Reviews.FindAsync(reviewId);

            if (review == null)
                throw new Exception("Review not found.");

            context.Reviews.Remove(review);

            await context.SaveChangesAsync();
        }

        public async Task<bool> HasPurchasedAsync(int userId, int productId)
        {
            return await context.OrderDetails
                .Include(x => x.Order)
                .AnyAsync(x =>
                    x.ProductId == productId &&
                    x.Order!.UserId == userId &&
                    x.Order.Status == "Completed");
        }

        public async Task<bool> HasReviewedAsync(int userId, int productId)
        {
            return await context.Reviews
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.ProductId == productId);
        }
    }
}