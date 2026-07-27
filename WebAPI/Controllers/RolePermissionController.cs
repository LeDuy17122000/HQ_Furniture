using Application.DTOs.RolePermission;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService service;

        public RolePermissionController(IRolePermissionService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        [HttpGet("Role/{roleId}")]
        public async Task<IActionResult> GetByRole(int roleId)
        {
            return Ok(await service.GetByRoleAsync(roleId));
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> Assign(AssignPermissionDto dto)
        {
            await service.AssignAsync(dto);

            return Ok("Assign Success");
        }

        [HttpDelete("{roleId}/{permissionId}")]
        public async Task<IActionResult> Remove(int roleId, int permissionId)
        {
            await service.RemoveAsync(roleId, permissionId);

            return Ok("Remove Success");
        }
    }
}