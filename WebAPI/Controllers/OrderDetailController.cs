using Application.DTOs.Order;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService service;

        public OrderDetailController(IOrderDetailService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("Order/{orderId}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            return Ok(await service.GetByOrderAsync(orderId));
        }

        [HttpGet("Product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            return Ok(await service.GetByProductAsync(productId));
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderDetailUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok("Updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return Ok("Deleted successfully.");
        }
    }
}