using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository repository;

        public ProductImageController(IProductImageRepository repository)
        {
            this.repository = repository;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await repository.GetAllAsync();
            return Ok(data);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await repository.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // GET IMAGE BY PRODUCT
        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var data = await repository.GetByProductAsync(productId);
            return Ok(data);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(ProductImage image)
        {
            await repository.AddAsync(image);
            await repository.SaveAsync();

            return Ok(image);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(ProductImage image)
        {
            await repository.UpdateAsync(image);
            await repository.SaveAsync();

            return Ok(image);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await repository.GetByIdAsync(id);

            if (image == null)
                return NotFound();

            await repository.DeleteAsync(image);
            await repository.SaveAsync();

            return Ok();
        }
    }
}