using Domain.DTOs.Dashboard;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardAsync();

        Task<List<RevenueDto>> GetRevenueAsync();

        Task<List<OrderStatisticDto>> GetOrderStatisticsAsync();

        Task<List<TopProductDto>> GetTopProductsAsync();
    }
}