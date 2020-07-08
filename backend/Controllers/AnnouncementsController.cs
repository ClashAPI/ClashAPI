using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    [Authorize(Roles = "Fejlesztő")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(UserManager<User> userManager, IAnnouncementService announcementService)
        {
            _userManager = userManager;
            _announcementService = announcementService;
        }

        // GET: api/Announcements
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAnnouncements()
        {
            return Ok(await _announcementService.GetAllAsync());
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(Guid id)
        {
            var announcement = await _announcementService.GetByIdAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            return Ok(announcement);
        }

        // PUT: api/Announcements/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute] Guid id, [FromBody] CreateAnnouncementDto createAnnouncementDto)
        {
            switch (createAnnouncementDto.Type)
            {
                case "info":
                    break;
                case "warning":
                    break;
                case "danger":
                    break;
                case "primary":
                    break;
                case "secondary":
                    break;
                default:
                    return BadRequest();
            }
            
            try
            {
                var announcement = await _announcementService.GetByIdAsync(id);
                announcement.Subject = createAnnouncementDto.Subject;
                announcement.Type = createAnnouncementDto.Type;

                await _announcementService.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _announcementService.GetIfExistsByIdAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Announcements
        [HttpPost]
        public async Task<ActionResult<Announcement>> PostAnnouncement(CreateAnnouncementDto createAnnouncementDto)
        {
            switch (createAnnouncementDto.Type)
            {
                case "info":
                    break;
                case "warning":
                    break;
                case "danger":
                    break;
                case "primary":
                    break;
                case "secondary":
                    break;
                default:
                    return BadRequest();
            }

            var announcement = new Announcement
            {
                Subject = createAnnouncementDto.Subject,
                Type = createAnnouncementDto.Type,
                Author = await _userManager.GetUserAsync(User)
            }; 
            
            var result = await _announcementService.AddAsync(announcement);
            await _announcementService.SaveAllAsync();

            return Ok(new {result.Entity.Id, userName = result.Entity.Author.UserName, result.Entity.Subject, result.Entity.Type, result.Entity.CreatedAt});
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Announcement>> DeleteAnnouncement(Guid id)
        {
            var announcement = await _announcementService.GetByIdNotFilteredAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _announcementService.Remove(announcement);
            await _announcementService.SaveAllAsync();

            return Ok();
        }
    }
}
