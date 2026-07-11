using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository repository;

        public PermissionController(IPermissionRepository repository)
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
            var permission = await repository.GetByIdAsync(id);

            if (permission == null)
                return NotFound();

            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Permission permission)
        {
            await repository.AddAsync(permission);
            await repository.SaveAsync();

            return Ok(permission);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Permission permission)
        {
            await repository.UpdateAsync(permission);
            await repository.SaveAsync();

            return Ok(permission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var permission = await repository.GetByIdAsync(id);

            if (permission == null)
                return NotFound();

            await repository.DeleteAsync(permission);
            await repository.SaveAsync();

            return Ok();
        }
    }
}