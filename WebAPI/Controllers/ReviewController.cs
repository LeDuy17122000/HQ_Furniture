using Application.DTOs.Review;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService service;

        public ReviewController(IReviewService service)
        {
            this.service = service;
        }

        // ==========================
        // ADMIN
        // ==========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }
        [HttpGet("Pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPending()
        {
            return Ok(await service.GetPendingAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            await service.ApproveAsync(id);
            return Ok("Review approved.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            await service.RejectAsync(id);
            return Ok("Review rejected.");
        }

        // ==========================
        // PUBLIC
        // ==========================

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await service.GetByIdAsync(id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [AllowAnonymous]
        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            return Ok(await service.GetByProductAsync(productId));
        }

        [Authorize]
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await service.GetByUserAsync(userId));
        }

        [AllowAnonymous]
        [HttpGet("Average/{productId}")]
        public async Task<IActionResult> GetAverage(int productId)
        {
            return Ok(await service.GetAverageRatingAsync(productId));
        }

        [AllowAnonymous]
        [HttpGet("Statistic/{productId}")]
        public async Task<IActionResult> GetStatistic(int productId)
        {
            return Ok(await service.GetStatisticAsync(productId));
        }

        // ==========================
        // CUSTOMER
        // ==========================

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Add(ReviewCreateDto dto)
        {
            await service.AddAsync(dto);
            return Ok("Review created, waiting for approval.");
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<IActionResult> Update(ReviewUpdateDto dto)
        {
            await service.UpdateAsync(dto);
            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return Ok();
        }
    }
}