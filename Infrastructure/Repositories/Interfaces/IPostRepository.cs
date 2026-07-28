using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetAllWithUserAsync();

        Task<Post?> GetDetailByIdAsync(int id);

        Task<List<Post>> GetByUserAsync(int userId);

        Task<List<Post>> SearchAsync(string keyword);
    }
}