using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository repository;

        public ReviewController(IReviewRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await repository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await repository.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var data = await repository.GetByProductAsync(productId);
            return Ok(data);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var data = await repository.GetByUserAsync(userId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Review review)
        {
            await repository.AddAsync(review);
            await repository.SaveAsync();

            return Ok(review);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Review review)
        {
            await repository.UpdateAsync(review);
            await repository.SaveAsync();

            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await repository.GetByIdAsync(id);

            if (review == null)
                return NotFound();

            await repository.DeleteAsync(review);
            await repository.SaveAsync();

            return Ok();
        }
    }
}