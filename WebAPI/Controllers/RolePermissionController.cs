using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionRepository repository;

        public RolePermissionController(IRolePermissionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(RolePermission rolePermission)
        {
            await repository.AddAsync(rolePermission);
            await repository.SaveAsync();

            return Ok(rolePermission);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RolePermission rolePermission)
        {
            await repository.DeleteAsync(rolePermission);
            await repository.SaveAsync();

            return Ok();
        }
    }
}