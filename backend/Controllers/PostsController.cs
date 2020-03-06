using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        private readonly UserManager<User> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper, IRepository repository)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                return Ok(await _context.Posts
                    .Select(i => new {i.Id, i.Author.UserName, i.Title, i.Content, i.IsVisible, i.CreatedAt, i.UpdatedAt})
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                return Ok(await _context.Posts
                    .Select(i => new { i.Id, i.Author.UserName, i.Title, i.Content, i.CreatedAt, i.UpdatedAt })
                    .FirstOrDefaultAsync(p => p.Id == id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                _repository.Delete(post);
                await _repository.SaveAllAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                post.Title = updatePostDto.Title;
                post.Content = updatePostDto.Content;
                post.IsVisible = updatePostDto.IsVisible;
                post.UpdatedAt = DateTime.Now;

                await _repository.SaveAllAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpPost("")]
        public async Task<IActionResult> AddPost(CreatePostDto createPostDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                createPostDto.Author = user;

                var post = _mapper.Map<Post>(createPostDto);

                var result = await _repository.AddAsync(post);
                await _repository.SaveAllAsync();

                return Ok(new {id = result.Entity.Id, userName = result.Entity.Author.UserName,
                    title = result.Entity.Title, content = result.Entity.Content, createdAt = result.Entity.CreatedAt});
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}