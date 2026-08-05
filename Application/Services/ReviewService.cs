using Application.DTOs.Review;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository repository;
        private readonly IMapper mapper;

        public ReviewService(
            IReviewRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ReviewDto>> GetAllAsync()
        {
            var reviews = await repository.GetAllWithDetailAsync();

            return mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto?> GetByIdAsync(int id)
        {
            var review = await repository.GetDetailByIdAsync(id);

            if (review == null)
                return null;

            return mapper.Map<ReviewDto>(review);
        }

        public async Task<List<ReviewDto>> GetByProductAsync(int productId)
        {
            var reviews = await repository.GetByProductAsync(productId);

            return mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task<List<ReviewDto>> GetByUserAsync(int userId)
        {
            var reviews = await repository.GetByUserAsync(userId);

            return mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            return await repository.GetAverageRatingAsync(productId);
        }

        public async Task<ReviewStatisticDto> GetStatisticAsync(int productId)
        {
            var reviews = await repository.GetByProductAsync(productId);

            return new ReviewStatisticDto
            {
                OneStar = reviews.Count(x => x.Rating == 1),
                TwoStar = reviews.Count(x => x.Rating == 2),
                ThreeStar = reviews.Count(x => x.Rating == 3),
                FourStar = reviews.Count(x => x.Rating == 4),
                FiveStar = reviews.Count(x => x.Rating == 5),

                TotalReview = reviews.Count,

                AverageRating = reviews.Any()
                    ? reviews.Average(x => x.Rating)
                    : 0
            };
        }

        public async Task AddAsync(ReviewCreateDto dto)
        {
            if (!await repository.HasPurchasedAsync(dto.UserId, dto.ProductId))
                throw new Exception("You have not purchased this product.");

            if (await repository.HasReviewedAsync(dto.UserId, dto.ProductId))
                throw new Exception("You have already reviewed this product.");

            var review = mapper.Map<Review>(dto);

            review.IsApproved = false;

            await repository.AddAsync(review);

            await repository.SaveAsync();
        }

        public async Task UpdateAsync(ReviewUpdateDto dto)
        {
            var review = await repository.GetByIdAsync(dto.ReviewId);

            if (review == null)
                throw new Exception("Review not found.");

            mapper.Map(dto, review);

            await repository.UpdateAsync(review);

            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await repository.GetByIdAsync(id);

            if (review == null)
                throw new Exception("Review not found.");

            await repository.DeleteAsync(review);

            await repository.SaveAsync();
        }

        // =========================
        // REVIEW AUTHORIZATION
        // =========================

        public async Task<List<ReviewDto>> GetPendingAsync()
        {
            var reviews = await repository.GetPendingAsync();

            return mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task ApproveAsync(int reviewId)
        {
            await repository.ApproveAsync(reviewId);
        }

        public async Task RejectAsync(int reviewId)
        {
            await repository.RejectAsync(reviewId);
        }

        public async Task<bool> HasPurchasedAsync(int userId, int productId)
        {
            return await repository.HasPurchasedAsync(userId, productId);
        }

        public async Task<bool> HasReviewedAsync(int userId, int productId)
        {
            return await repository.HasReviewedAsync(userId, productId);
        }
    }
}