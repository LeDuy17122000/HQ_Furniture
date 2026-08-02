using Application.DTOs.Order;

namespace Application.Interfaces
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetailDto>> GetByOrderAsync(int orderId);

        Task<List<OrderDetailDto>> GetByProductAsync(int productId);

        Task<OrderDetailDto?> GetByIdAsync(int id);

        Task UpdateAsync(OrderDetailUpdateDto dto);

        Task DeleteAsync(int id);
    }
}