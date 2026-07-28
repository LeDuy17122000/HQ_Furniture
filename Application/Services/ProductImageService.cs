using Application.DTOs.ProductImage;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository repository;
        private readonly IMapper mapper;

        public ProductImageService(
            IProductImageRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET ALL
        public async Task<List<ProductImageDto>> GetAllAsync()
        {
            var images = await repository.GetAllAsync();

            return mapper.Map<List<ProductImageDto>>(images);
        }

        // GET BY ID
        public async Task<ProductImageDto?> GetByIdAsync(int id)
        {
            var image = await repository.GetByIdAsync(id);

            if (image == null)
                return null;

            return mapper.Map<ProductImageDto>(image);
        }

        // GET BY PRODUCT
        public async Task<List<ProductImageDto>> GetByProductAsync(int productId)
        {
            var images = await repository.GetByProductAsync(productId);

            return mapper.Map<List<ProductImageDto>>(images);
        }

        // GET MAIN IMAGE
        public async Task<ProductImageDto?> GetMainImageAsync(int productId)
        {
            var image = await repository.GetMainImageAsync(productId);

            if (image == null)
                return null;

            return mapper.Map<ProductImageDto>(image);
        }

        // ADD
        public async Task AddAsync(ProductImageCreateDto dto)
        {
            var image = mapper.Map<ProductImage>(dto);

            await repository.AddAsync(image);

            await repository.SaveAsync();
        }

        // UPDATE
        public async Task UpdateAsync(ProductImageUpdateDto dto)
        {
            var image = await repository.GetByIdAsync(dto.ImageId);

            if (image == null)
                throw new Exception("Image not found.");

            mapper.Map(dto, image);

            await repository.UpdateAsync(image);

            await repository.SaveAsync();
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var image = await repository.GetByIdAsync(id);

            if (image == null)
                throw new Exception("Image not found.");

            await repository.DeleteAsync(image);

            await repository.SaveAsync();
        }
    }
}