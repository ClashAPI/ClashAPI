using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services.Impl
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Announcement
        {
            return await _announcementRepository.AddAsync(entity);
        }

        public void Remove<T>(T entity) where T : Announcement
        {
            _announcementRepository.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _announcementRepository.SaveAllAsync();
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            return await _announcementRepository.GetAllAsync();
        }

        public async Task<AnnouncementDto> GetByIdAsync(Guid id)
        {
            return await _announcementRepository.GetByIdAsync(id);
        }

        public async Task<bool> GetIfExistsByIdAsync(Guid id)
        {
            var result = await _announcementRepository.GetByIdAsync(id);
            return result != null;
        }

        public async Task<Announcement> GetByIdNotFilteredAsync(Guid id)
        {
            return await _announcementRepository.GetByIdNotFilteredAsync(id);
        }
    }
}