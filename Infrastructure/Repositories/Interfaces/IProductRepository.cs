using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetByCategoryAsync(int categoryId);

        Task<List<Product>> SearchAsync(string keyword);
    }
}