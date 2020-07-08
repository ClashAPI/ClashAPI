using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace backend.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICRAccountService _crAccountService;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository userRepository, 
            ICRAccountService crAccountService, IRoleService roleService)
        {
            _userRepository = userRepository;
            _crAccountService = crAccountService;
            _roleService = roleService;
        }

        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : User
        {
            return await _userRepository.AddAsync(entity);
        }
        
        public void Remove<T>(T entity) where T : User
        {
            _userRepository.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _userRepository.SaveAllAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<IEnumerable<string>> GetBadgesByPlayerTagAsync(string playerTag)
        {
            var account = await _crAccountService.GetByPlayerTagAsync(playerTag);

            if (account == null)
            {
                return null;
            }
            
            IEnumerable<string> badges = new LinkedList<string>();
            badges = badges.Append("verified");
            var user = await _userRepository.GetByIdAsync(account.User.Id);
            var devRole = await _roleService.GetByNameAsync("Fejlesztő");

            if (user.UserRoles.FirstOrDefault(r => r.Role.Id == devRole.Id) != null)
            {
                badges = badges.Append("staff");
            }

            return badges;
        }
        
        public async Task<IEnumerable<User>> GetAllRegisteredTodayAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Where(u => u.CreatedAt.Day == DateTime.Today.Day);
        }
    }
}