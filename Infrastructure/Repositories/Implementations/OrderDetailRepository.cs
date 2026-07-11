using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderDetailRepository
        : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<List<OrderDetail>> GetByOrderAsync(int orderId)
        {
            return await dbSet
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<List<OrderDetail>> GetByProductAsync(int productId)
        {
            return await dbSet
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}