using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IProductImageRepository
        : IRepository<ProductImage>
    {
        Task<List<ProductImage>> GetByProductAsync(int productId);

        Task<ProductImage?> GetMainImageAsync(int productId);
    }
}