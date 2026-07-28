using Application.DTOs.Post;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAllAsync();

        Task<PostDto?> GetByIdAsync(int id);

        Task<List<PostDto>> GetByUserAsync(int userId);

        Task<List<PostDto>> SearchAsync(string keyword);

        Task AddAsync(PostCreateDto dto);

        Task UpdateAsync(PostUpdateDto dto);

        Task DeleteAsync(int id);
    }
}