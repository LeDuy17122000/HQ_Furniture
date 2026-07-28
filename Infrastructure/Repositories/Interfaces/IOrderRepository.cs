using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetAllWithUserAsync();

        Task<Order?> GetDetailByIdAsync(int id);

        Task<List<Order>> GetByUserAsync(int userId);

        Task<List<Order>> GetByStatusAsync(string status);
    }
}