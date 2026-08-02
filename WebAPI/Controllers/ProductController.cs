using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        [HasPermission("Product.View")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [HasPermission("Product.View")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("Category/{categoryId}")]
        [HasPermission("Product.View")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var data = await service.GetByCategoryAsync(categoryId);

            return Ok(data);
        }

        [HttpGet("Search")]
        [HasPermission("Product.View")]
        public async Task<IActionResult> Search(string keyword)
        {
            var data = await service.SearchAsync(keyword);

            return Ok(data);
        }

        [HttpPost]
        [HasPermission("Product.Create")]
        public async Task<IActionResult> Add(ProductCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok(new
            {
                Message = "Product created successfully."
            });
        }

        [HttpPut]
        [HasPermission("Product.Update")]
        public async Task<IActionResult> Update(ProductUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok(new
            {
                Message = "Product updated successfully."
            });
        }

        [HttpDelete("{id}")]
        [HasPermission("Product.Delete")]
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