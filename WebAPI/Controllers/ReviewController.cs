using Application.DTOs.Review;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService service;

        public ReviewController(IReviewService service)
        {
            this.service = service;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await service.GetByIdAsync(id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // GET BY PRODUCT
        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            return Ok(await service.GetByProductAsync(productId));
        }

        // GET BY USER
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await service.GetByUserAsync(userId));
        }

        // GET AVERAGE
        [HttpGet("Average/{productId}")]
        public async Task<IActionResult> GetAverage(int productId)
        {
            return Ok(await service.GetAverageRatingAsync(productId));
        }

        // GET STATISTIC
        [HttpGet("Statistic/{productId}")]
        public async Task<IActionResult> GetStatistic(int productId)
        {
            return Ok(await service.GetStatisticAsync(productId));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(ReviewCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok();
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(ReviewUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return Ok();
        }
    }
}