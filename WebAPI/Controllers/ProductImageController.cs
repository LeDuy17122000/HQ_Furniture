using Application.DTOs.ProductImage;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService service;

        public ProductImageController(IProductImageService service)
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
            var image = await service.GetByIdAsync(id);

            if (image == null)
                return NotFound();

            return Ok(image);
        }

        // GET BY PRODUCT
        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            return Ok(await service.GetByProductAsync(productId));
        }

        // GET MAIN IMAGE
        [HttpGet("Main/{productId}")]
        public async Task<IActionResult> GetMainImage(int productId)
        {
            var image = await service.GetMainImageAsync(productId);

            if (image == null)
                return NotFound();

            return Ok(image);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(ProductImageCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok();
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(ProductImageUpdateDto dto)
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