using Application.DTOs.Product;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductService(
            IProductRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET ALL
        public async Task<List<ProductListDto>> GetAllAsync()
        {
            var products = await repository.GetAllAsync();

            return mapper.Map<List<ProductListDto>>(products);
        }

        // GET BY ID
        public async Task<ProductDetailDto?> GetByIdAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return mapper.Map<ProductDetailDto>(product);
        }

        // GET BY CATEGORY
        public async Task<List<ProductListDto>> GetByCategoryAsync(int categoryId)
        {
            var products = await repository.GetByCategoryAsync(categoryId);

            return mapper.Map<List<ProductListDto>>(products);
        }

        // SEARCH
        public async Task<List<ProductListDto>> SearchAsync(string keyword)
        {
            var products = await repository.SearchAsync(keyword);

            return mapper.Map<List<ProductListDto>>(products);
        }

        // CREATE
        public async Task AddAsync(ProductCreateDto dto)
        {
            var product = mapper.Map<Product>(dto);

            await repository.AddAsync(product);
            await repository.SaveAsync();
        }

        // UPDATE
        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var product = await repository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            mapper.Map(dto, product);

            await repository.UpdateAsync(product);
            await repository.SaveAsync();
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product == null)
                throw new Exception("Product not found.");

            await repository.DeleteAsync(product);
            await repository.SaveAsync();
        }
    }
}