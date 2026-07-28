using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderDetailRepository
        : IRepository<OrderDetail>
    {
        Task<List<OrderDetail>> GetByOrderAsync(int orderId);

        Task<List<OrderDetail>> GetByProductAsync(int productId);

        Task<OrderDetail?> GetDetailByIdAsync(int id);
    }
}