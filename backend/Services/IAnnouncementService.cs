using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services
{
    public interface IAnnouncementService
    {
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : Announcement;
        void Remove<T>(T entity) where T : Announcement;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AnnouncementDto>> GetAllAsync();
        Task<AnnouncementDto> GetByIdAsync(Guid id);
        Task<bool> GetIfExistsByIdAsync(Guid id);
        Task<Announcement> GetByIdNotFilteredAsync(Guid id);
    }
}