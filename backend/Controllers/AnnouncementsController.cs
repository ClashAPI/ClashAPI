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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    [Authorize(Roles = "Fejlesztő")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AnnouncementsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Announcements
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAnnouncements()
        {
            return Ok(await _context.Announcements.Select(p => new { p.Id, p.Subject, p.Type, p.Author.UserName, p.CreatedAt }).ToListAsync());
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(Guid id)
        {
            var announcement = await _context.Announcements.Select(p => new {p.Id, p.Subject, p.Type, p.Author.UserName, p.CreatedAt})
                .FirstOrDefaultAsync(p => p.Id == id);

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
        public async Task<IActionResult> PutAnnouncement(Guid id, CreateAnnouncementDto createAnnouncementDto)
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
                var announcement = await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);
                announcement.Subject = createAnnouncementDto.Subject;
                announcement.Type = createAnnouncementDto.Type;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
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
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();

            return Ok(new {announcement.Id, userName = announcement.Author.UserName, announcement.Subject, announcement.Type, announcement.CreatedAt});
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Announcement>> DeleteAnnouncement(Guid id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AnnouncementExists(Guid id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
    }
}
