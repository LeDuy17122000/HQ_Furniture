using Application.DTOs.Order;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // GET BY USER
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await service.GetByUserAsync(userId));
        }

        // GET BY STATUS
        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            return Ok(await service.GetByStatusAsync(status));
        }

        // CREATE ORDER
        [HttpPost]
        public async Task<IActionResult> Add(OrderCreateDto dto)
        {
            await service.AddAsync(dto);

            return Ok("Order created successfully.");
        }

        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Update(OrderUpdateDto dto)
        {
            await service.UpdateAsync(dto);

            return Ok("Order updated successfully.");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return Ok("Order deleted successfully.");
        }
        [HttpPut("Confirm/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Confirm(int id)
        {
            await service.ConfirmAsync(id);

            return Ok(new
            {
                message = "Order confirmed."
            });
        }

        [HttpPut("Shipping/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Shipping(int id)
        {
            await service.ShippingAsync(id);

            return Ok(new
            {
                message = "Order is shipping."
            });
        }

        [HttpPut("Complete/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Complete(int id)
        {
            await service.CompleteAsync(id);

            return Ok(new
            {
                message = "Order completed."
            });
        }

        [HttpPut("Cancel/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Cancel(int id)
        {
            await service.CancelAsync(id);

            return Ok(new
            {
                message = "Order cancelled."
            });
        }
    }
}