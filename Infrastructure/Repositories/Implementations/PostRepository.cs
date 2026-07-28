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

        public async Task<List<Post>> GetAllWithUserAsync()
        {
            return await context.Posts
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<Post?> GetDetailByIdAsync(int id)
        {
            return await context.Posts
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.PostId == id);
        }

        public async Task<List<Post>> GetByUserAsync(int userId)
        {
            return await context.Posts
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Post>> SearchAsync(string keyword)
        {
            return await context.Posts
                .Include(x => x.User)
                .Where(x => x.Title.Contains(keyword))
                .ToListAsync();
        }
    }
}