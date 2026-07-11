using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository
        : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<List<Order>> GetByUserAsync(int userId)
        {
            return await dbSet
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByStatusAsync(string status)
        {
            return await dbSet
                .Where(x => x.Status == status)
                .ToListAsync();
        }
    }
}