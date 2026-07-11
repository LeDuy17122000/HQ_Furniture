using Application.DTOs.Product;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllAsync();

        Task<ProductDetailDto?> GetByIdAsync(int id);

        Task AddAsync(ProductCreateDto dto);

        Task UpdateAsync(ProductUpdateDto dto);

        Task DeleteAsync(int id);

        Task<List<ProductListDto>> SearchAsync(string keyword);

        Task<List<ProductListDto>> GetByCategoryAsync(int categoryId);
    }
}