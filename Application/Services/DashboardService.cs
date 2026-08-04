using Domain.DTOs.Dashboard;
using Application.Interfaces;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository repository;

        public DashboardService(IDashboardRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return await repository.GetDashboardAsync();
        }

        public async Task<List<RevenueDto>> GetRevenueAsync()
        {
            return await repository.GetRevenueAsync();
        }

        public async Task<List<OrderStatisticDto>> GetOrderStatisticsAsync()
        {
            return await repository.GetOrderStatisticsAsync();
        }

        public async Task<List<TopProductDto>> GetTopProductsAsync()
        {
            return await repository.GetTopProductsAsync();
        }
    }
}