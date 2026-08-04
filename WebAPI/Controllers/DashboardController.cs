using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.Dashboard;
namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService service;

        public DashboardController(IDashboardService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            return Ok(await service.GetDashboardAsync());
        }

        [HttpGet("Revenue")]
        public async Task<IActionResult> GetRevenue()
        {
            return Ok(await service.GetRevenueAsync());
        }

        [HttpGet("OrderStatistics")]
        public async Task<IActionResult> GetOrderStatistics()
        {
            return Ok(await service.GetOrderStatisticsAsync());
        }

        [HttpGet("TopProducts")]
        public async Task<IActionResult> GetTopProducts()
        {
            return Ok(await service.GetTopProductsAsync());
        }
    }
}