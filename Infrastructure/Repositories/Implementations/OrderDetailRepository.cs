using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class OrderDetailRepository
        : Repository<OrderDetail>,
          IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<OrderDetail>> GetByOrderAsync(int orderId)
        {
            return await context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<List<OrderDetail>> GetByProductAsync(int productId)
        {
            return await context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetDetailByIdAsync(int id)
        {
            return await context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.OrderDetailId == id);
        }
    }
}