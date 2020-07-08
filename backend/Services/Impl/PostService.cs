using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services.Impl
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Post
        {
            return await _postRepository.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : Post
        {
            _postRepository.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _postRepository.SaveAllAsync();
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Post>> GetByUserIdAsync(Guid id)
        {
            return await _postRepository.GetByUserIdAsync(id);
        }
    }
}