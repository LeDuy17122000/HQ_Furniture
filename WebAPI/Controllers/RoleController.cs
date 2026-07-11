using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository repository;

        public RoleController(IRoleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await repository.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Role role)
        {
            await repository.AddAsync(role);
            await repository.SaveAsync();

            return Ok(role);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Role role)
        {
            await repository.UpdateAsync(role);
            await repository.SaveAsync();

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await repository.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            await repository.DeleteAsync(role);
            await repository.SaveAsync();

            return Ok();
        }
    }
}