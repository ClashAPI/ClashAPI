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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Announcement
        {
            return await _context.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : Announcement
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            var announcements = await _context.Announcements.ToListAsync();
            IEnumerable<AnnouncementDto> filteredAnnouncements = new LinkedList<AnnouncementDto>();

            foreach (var announcement in announcements)
            {
                filteredAnnouncements = filteredAnnouncements.Append(new AnnouncementDto
                {
                    Id = announcement.Id,
                    Subject = announcement.Subject,
                    Type = announcement.Type,
                    CreatedAt = announcement.CreatedAt,
                    UserName = announcement.Author.UserName
                });
            }

            return filteredAnnouncements;
        }

        public async Task<AnnouncementDto> GetByIdAsync(Guid id)
        {
            var announcement = await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);

            var filteredAnnouncement = new AnnouncementDto
            {
                Id = announcement.Id,
                Subject = announcement.Subject,
                Type = announcement.Type,
                CreatedAt = announcement.CreatedAt,
                UserName = announcement.Author.UserName
            };

            return filteredAnnouncement;
        }

        public async Task<Announcement> GetByIdNotFilteredAsync(Guid id)
        {
            return await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}