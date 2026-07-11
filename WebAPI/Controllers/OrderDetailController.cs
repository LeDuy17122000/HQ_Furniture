using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository repository;

        public OrderDetailController(IOrderDetailRepository repository)
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
            var detail = await repository.GetByIdAsync(id);

            if (detail == null)
                return NotFound();

            return Ok(detail);
        }

        // GET BY ORDER
        [HttpGet("Order/{orderId}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            return Ok(await repository.GetByOrderAsync(orderId));
        }

        // GET BY PRODUCT
        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            return Ok(await repository.GetByProductAsync(productId));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(OrderDetail detail)
        {
            await repository.AddAsync(detail);
            await repository.SaveAsync();

            return Ok(detail);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(OrderDetail detail)
        {
            await repository.UpdateAsync(detail);
            await repository.SaveAsync();

            return Ok(detail);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var detail = await repository.GetByIdAsync(id);

            if (detail == null)
                return NotFound();

            await repository.DeleteAsync(detail);
            await repository.SaveAsync();

            return Ok();
        }
    }
}