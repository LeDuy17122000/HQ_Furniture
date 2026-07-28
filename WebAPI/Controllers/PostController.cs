using Application.DTOs.Post;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;

        public PostController(IPostService service)
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
            var post = await service.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await service.GetByUserAsync(userId));
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            return Ok(await service.SearchAsync(keyword));
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PostUpdateDto dto)
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