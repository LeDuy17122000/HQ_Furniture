using Application.DTOs.Post;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        private readonly IMapper mapper;

        public PostService(
            IPostRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PostDto>> GetAllAsync()
        {
            var posts = await repository.GetAllWithUserAsync();

            return mapper.Map<List<PostDto>>(posts);
        }

        public async Task<PostDto?> GetByIdAsync(int id)
        {
            var post = await repository.GetDetailByIdAsync(id);

            if (post == null)
                return null;

            return mapper.Map<PostDto>(post);
        }

        public async Task<List<PostDto>> GetByUserAsync(int userId)
        {
            var posts = await repository.GetByUserAsync(userId);

            return mapper.Map<List<PostDto>>(posts);
        }

        public async Task<List<PostDto>> SearchAsync(string keyword)
        {
            var posts = await repository.SearchAsync(keyword);

            return mapper.Map<List<PostDto>>(posts);
        }

        public async Task AddAsync(PostCreateDto dto)
        {
            var post = mapper.Map<Post>(dto);

            await repository.AddAsync(post);

            await repository.SaveAsync();
        }

        public async Task UpdateAsync(PostUpdateDto dto)
        {
            var post = await repository.GetByIdAsync(dto.PostId);

            if (post == null)
                throw new Exception("Post not found.");

            mapper.Map(dto, post);

            post.UpdatedDate = DateTime.Now;

            await repository.UpdateAsync(post);

            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await repository.GetByIdAsync(id);

            if (post == null)
                throw new Exception("Post not found.");

            await repository.DeleteAsync(post);

            await repository.SaveAsync();
        }
    }
}