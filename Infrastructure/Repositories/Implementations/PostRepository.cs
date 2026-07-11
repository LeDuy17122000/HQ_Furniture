using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class PostRepository
        : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<List<Post>> GetByUserAsync(int userId)
        {
            return await dbSet
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Post>> SearchAsync(string keyword)
        {
            return await dbSet
                .Where(x =>
                    x.Title.Contains(keyword) ||
                    (x.Content != null && x.Content.Contains(keyword)))
                .ToListAsync();
        }
    }
}