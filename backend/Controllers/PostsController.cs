using System;
using System.Threading.Tasks;
using AutoMapper;
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public PostsController(UserManager<User> userManager, 
            IMapper mapper, IPostService postService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                return Ok(await _postService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            try
            {
                return Ok(await _postService.GetByIdAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                _postService.Remove(post);
                await _postService.SaveAllAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Roles = "Fejlesztő")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, UpdatePostDto updatePostDto)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                post.Title = updatePostDto.Title;
                post.Content = updatePostDto.Content;
                post.IsVisible = updatePostDto.IsVisible;
                post.UpdatedAt = DateTime.Now;

                await _postService.SaveAllAsync();

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

                var result = await _postService.AddAsync(post);
                await _postService.SaveAllAsync();

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