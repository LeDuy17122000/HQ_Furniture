using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        // GET
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

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            await repository.AddAsync(category);
            await repository.SaveAsync();

            return Ok(category);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            await repository.UpdateAsync(category);
            await repository.SaveAsync();

            return Ok(category);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cate = await repository.GetByIdAsync(id);

            if (cate == null)
                return NotFound();

            await repository.DeleteAsync(cate);
            await repository.SaveAsync();

            return Ok();
        }
    }
}