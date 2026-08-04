using Domain.DTOs.Dashboard;

namespace Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();

        Task<List<RevenueDto>> GetRevenueAsync();

        Task<List<OrderStatisticDto>> GetOrderStatisticsAsync();

        Task<List<TopProductDto>> GetTopProductsAsync();
    }
}