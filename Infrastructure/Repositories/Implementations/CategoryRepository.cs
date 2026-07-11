using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class CategoryRepository
        : Repository<Category>,
          ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await dbSet
                .FirstOrDefaultAsync(x => x.CategoryName == name);
        }
    }
}