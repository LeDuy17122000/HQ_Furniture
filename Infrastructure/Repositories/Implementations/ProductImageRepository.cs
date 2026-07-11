using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class ProductImageRepository
        : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<List<ProductImage>> GetByProductAsync(int productId)
        {
            return await dbSet
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}