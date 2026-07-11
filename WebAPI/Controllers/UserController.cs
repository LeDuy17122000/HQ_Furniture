using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repository.GetAllAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await repository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET USER BY ROLE
        [HttpGet("Role/{roleId}")]
        public async Task<IActionResult> GetByRole(int roleId)
        {
            var data = await repository.GetByRoleAsync(roleId);

            return Ok(data);
        }

        // SEARCH
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var data = await repository.SearchAsync(keyword);

            return Ok(data);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            await repository.AddAsync(user);
            await repository.SaveAsync();

            return Ok(user);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await repository.UpdateAsync(user);
            await repository.SaveAsync();

            return Ok(user);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await repository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            await repository.DeleteAsync(user);
            await repository.SaveAsync();

            return Ok();
        }
    }
}