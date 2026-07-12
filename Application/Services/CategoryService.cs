using Application.DTOs.Category;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        private readonly IMapper mapper;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var data = await repository.GetAllAsync();

            return mapper.Map<List<CategoryDto>>(data);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var data = await repository.GetByIdAsync(id);

            if (data == null)
                return null;

            return mapper.Map<CategoryDto>(data);
        }

        public async Task AddAsync(CategoryCreateDto dto)
        {
            var entity = mapper.Map<Category>(dto);

            await repository.AddAsync(entity);

            await repository.SaveAsync();
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            var entity = await repository.GetByIdAsync(dto.CategoryId);

            if (entity == null)
                throw new Exception("Category not found.");

            mapper.Map(dto, entity);

            await repository.UpdateAsync(entity);

            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity == null)
                throw new Exception("Category not found.");

            await repository.DeleteAsync(entity);

            await repository.SaveAsync();
        }
    }
}