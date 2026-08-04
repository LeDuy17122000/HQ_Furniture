using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class OrderRepository
        : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<Order>> GetAllWithUserAsync()
        {
            return await context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ToListAsync();
        }

        public async Task<Order?> GetDetailByIdAsync(int id)
        {
            return await context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<List<Order>> GetByUserAsync(int userId)
        {
            return await context.Orders
                .Include(x => x.OrderDetails)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByStatusAsync(string status)
        {
            return await context.Orders
                .Include(x => x.User)
                .Where(x => x.Status == status)
                .ToListAsync();
        }
        public async Task ConfirmAsync(int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Order not found.");

            if (order.Status != "Pending")
                throw new Exception("Only Pending orders can be confirmed.");

            order.Status = "Confirmed";

            await context.SaveChangesAsync();
        }

        public async Task ShippingAsync(int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Order not found.");

            if (order.Status != "Confirmed")
                throw new Exception("Only Confirmed orders can be shipped.");

            order.Status = "Shipping";

            await context.SaveChangesAsync();
        }

        public async Task CompleteAsync(int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Order not found.");

            if (order.Status != "Shipping")
                throw new Exception("Only Shipping orders can be completed.");

            order.Status = "Completed";

            await context.SaveChangesAsync();
        }

        public async Task CancelAsync(int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);

            if (order == null)
                throw new Exception("Order not found.");

            if (order.Status == "Completed")
                throw new Exception("Completed orders cannot be cancelled.");

            if (order.Status == "Cancelled")
                throw new Exception("Order already cancelled.");

            order.Status = "Cancelled";

            await context.SaveChangesAsync();
        }
    }
}