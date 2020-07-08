using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Repositories.Impl
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Post
        {
            return await _context.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : Post
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            IEnumerable<PostDto> filteredPosts = new LinkedList<PostDto>();

            foreach (var post in posts)
            {
                filteredPosts = filteredPosts.Append(new PostDto
                {
                    UserName = post.Author.UserName,
                    Content = post.Content,
                    Title = post.Title,
                    CreatedAt = post.CreatedAt,
                    Id = post.Id,
                    IsVisible = post.IsVisible,
                    UpdatedAt = post.UpdatedAt
                });
            }

            return filteredPosts;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetByUserIdAsync(Guid id)
        {
            return await _context.Posts.Where(p => p.Author.Id == id).ToListAsync();
        }
    }
}