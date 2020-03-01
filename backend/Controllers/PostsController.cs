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
        private readonly UserManager<User> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                _context.Remove(post);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
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

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> AddPost(CreatePostDto createPostDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                createPostDto.Author = user;

                var post = _mapper.Map<Post>(createPostDto);

                var result = await _context.AddAsync(post);
                await _context.SaveChangesAsync();

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