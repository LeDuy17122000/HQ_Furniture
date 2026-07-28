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
    }
}