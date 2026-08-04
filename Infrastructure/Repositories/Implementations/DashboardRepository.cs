using Domain.DTOs.Dashboard;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext context;

        public DashboardRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            return new DashboardDto
            {
                TotalUsers = await context.Users.CountAsync(),

                TotalProducts = await context.Products.CountAsync(),

                TotalCategories = await context.Categories.CountAsync(),

                TotalOrders = await context.Orders.CountAsync(),

                TotalRevenue = await context.Orders
                    .Where(x => x.Status == "Completed")
                    .SumAsync(x => (decimal?)x.TotalAmount) ?? 0
            };
        }

        public async Task<List<RevenueDto>> GetRevenueAsync()
        {
            return await context.Orders
                .Where(x => x.Status == "Completed")
                .GroupBy(x => new
                {
                    x.OrderDate.Year,
                    x.OrderDate.Month
                })
                .Select(x => new RevenueDto
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Revenue = x.Sum(o => o.TotalAmount)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();
        }

        public async Task<List<OrderStatisticDto>> GetOrderStatisticsAsync()
        {
            return await context.Orders
                .GroupBy(x => x.Status)
                .Select(x => new OrderStatisticDto
                {
                    Status = x.Key,
                    TotalOrders = x.Count()
                })
                .ToListAsync();
        }

        public async Task<List<TopProductDto>> GetTopProductsAsync()
        {
            return await context.OrderDetails
                .GroupBy(x => new
                {
                    x.ProductId,
                    x.Product.ProductName
                })
                .Select(x => new TopProductDto
                {
                    ProductId = x.Key.ProductId,
                    ProductName = x.Key.ProductName,
                    TotalSold = x.Sum(i => i.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync();
        }
    }
}