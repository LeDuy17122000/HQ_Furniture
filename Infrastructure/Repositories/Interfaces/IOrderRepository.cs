using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetAllWithUserAsync();

        Task<Order?> GetDetailByIdAsync(int id);

        Task<List<Order>> GetByUserAsync(int userId);

        Task<List<Order>> GetByStatusAsync(string status);
        Task ConfirmAsync(int orderId);

        Task ShippingAsync(int orderId);

        Task CompleteAsync(int orderId);

        Task CancelAsync(int orderId);
    }
}