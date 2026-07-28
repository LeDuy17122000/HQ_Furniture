using Application.DTOs.Order;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(int id);

        Task<List<OrderDto>> GetByUserAsync(int userId);

        Task<List<OrderDto>> GetByStatusAsync(string status);

        Task AddAsync(OrderCreateDto dto);

        Task UpdateAsync(OrderUpdateDto dto);

        Task DeleteAsync(int id);
    }
}