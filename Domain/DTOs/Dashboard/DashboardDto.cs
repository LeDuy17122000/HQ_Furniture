namespace Domain.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalUsers { get; set; }

    public int TotalProducts { get; set; }

    public int TotalCategories { get; set; }

    public int TotalOrders { get; set; }

    public decimal TotalRevenue { get; set; }
}