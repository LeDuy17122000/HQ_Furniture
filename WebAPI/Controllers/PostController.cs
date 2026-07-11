using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository repository;

        public PostController(IPostRepository repository)
        {
            this.repository = repository;
        }

        // GET ALL
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

        // GET BY USER
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var data = await repository.GetByUserAsync(userId);

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
        public async Task<IActionResult> Add(Post post)
        {
            await repository.AddAsync(post);
            await repository.SaveAsync();

            return Ok(post);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(Post post)
        {
            await repository.UpdateAsync(post);
            await repository.SaveAsync();

            return Ok(post);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await repository.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            await repository.DeleteAsync(post);
            await repository.SaveAsync();

            return Ok();
        }
    }
}