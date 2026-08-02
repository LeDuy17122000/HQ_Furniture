using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        // ================= GET ALL =================
        [HttpGet]
        [HasPermission("User.View")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.GetAllAsync();

            return Ok(data);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        [HasPermission("User.View")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // ================= CREATE =================
        [HttpPost]
        [HasPermission("User.Create")]
        public async Task<IActionResult> Add(UserCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok(new
            {
                Message = "User created successfully."
            });
        }

        // ================= UPDATE =================
        [HttpPut]
        [HasPermission("User.Update")]
        public async Task<IActionResult> Update(UserUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok(new
            {
                Message = "User updated successfully."
            });
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        [HasPermission("User.Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return Ok(new
            {
                Message = "User deleted successfully."
            });
        }

        // ================= CHANGE ROLE =================
        [HttpPut("ChangeRole")]
        [HasPermission("User.UpdateRole")]
        public async Task<IActionResult> ChangeRole(UserChangeRoleDto dto)
        {
            await service.ChangeRoleAsync(dto);

            return Ok(new
            {
                Message = "Change role successfully."
            });
        }
    }
}