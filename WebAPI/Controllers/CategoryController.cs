using Application.DTOs.Category;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return Ok();
        }
    }
}