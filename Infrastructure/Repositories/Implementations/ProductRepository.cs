using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class ProductRepository
        : Repository<Product>,
          IProductRepository
    {
        public ProductRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<List<Product>> GetByCategoryAsync(int categoryId)
        {
            return await dbSet
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<Product>> SearchAsync(string keyword)
        {
            return await dbSet
                .Where(x => x.ProductName.Contains(keyword))
                .ToListAsync();
        }
        public override async Task<List<Product>> GetAllAsync()
        {
            return await dbSet
                .Include(p => p.Category)
                .ToListAsync();
        }

        public override async Task<Product?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }
    }
}