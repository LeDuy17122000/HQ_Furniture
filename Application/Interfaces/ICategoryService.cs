using Application.DTOs.Category;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task AddAsync(CategoryCreateDto dto);

        Task UpdateAsync(CategoryUpdateDto dto);

        Task DeleteAsync(int id);
    }
}