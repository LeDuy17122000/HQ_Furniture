using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repository;

        public OrderController(IOrderRepository repository)
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
            var order = await repository.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // GET BY USER
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await repository.GetByUserAsync(userId));
        }

        // GET BY STATUS
        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            return Ok(await repository.GetByStatusAsync(status));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(Order order)
        {
            await repository.AddAsync(order);
            await repository.SaveAsync();

            return Ok(order);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            await repository.UpdateAsync(order);
            await repository.SaveAsync();

            return Ok(order);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await repository.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            await repository.DeleteAsync(order);
            await repository.SaveAsync();

            return Ok();
        }
    }
}