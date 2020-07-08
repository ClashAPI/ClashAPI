using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services
{
    public interface IPostService
    {
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Post;
        void Remove<T>(T entity) where T : Post;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<PostDto>> GetAllAsync();
        Task<Post> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetByUserIdAsync(Guid id);
    }
}