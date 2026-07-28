using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetAllWithDetailAsync();

        Task<Review?> GetDetailByIdAsync(int id);

        Task<List<Review>> GetByProductAsync(int productId);

        Task<List<Review>> GetByUserAsync(int userId);

        Task<double> GetAverageRatingAsync(int productId);
    }
}