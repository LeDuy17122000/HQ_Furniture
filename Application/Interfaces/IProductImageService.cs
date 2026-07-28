using Application.DTOs.ProductImage;

namespace Application.Interfaces
{
    public interface IProductImageService
    {
        Task<List<ProductImageDto>> GetAllAsync();

        Task<ProductImageDto?> GetByIdAsync(int id);

        Task<List<ProductImageDto>> GetByProductAsync(int productId);

        Task<ProductImageDto?> GetMainImageAsync(int productId);

        Task AddAsync(ProductImageCreateDto dto);

        Task UpdateAsync(ProductImageUpdateDto dto);

        Task DeleteAsync(int id);
    }
}