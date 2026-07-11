using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.GetAllAsync();
            return Ok(data);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // GET BY CATEGORY
        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var data = await service.GetByCategoryAsync(categoryId);

            return Ok(data);
        }

        // SEARCH
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var data = await service.SearchAsync(keyword);

            return Ok(data);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok(new
            {
                Message = "Product created successfully."
            });
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok(new
            {
                Message = "Product updated successfully."
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return Ok(new
            {
                Message = "Product deleted successfully."
            });
        }
    }
}