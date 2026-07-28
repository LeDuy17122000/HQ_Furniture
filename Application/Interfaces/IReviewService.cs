using Application.DTOs.Review;

namespace Application.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllAsync();

        Task<ReviewDto?> GetByIdAsync(int id);

        Task<List<ReviewDto>> GetByProductAsync(int productId);

        Task<List<ReviewDto>> GetByUserAsync(int userId);

        Task<double> GetAverageRatingAsync(int productId);

        Task<ReviewStatisticDto> GetStatisticAsync(int productId);

        Task AddAsync(ReviewCreateDto dto);

        Task UpdateAsync(ReviewUpdateDto dto);

        Task DeleteAsync(int id);
    }
}