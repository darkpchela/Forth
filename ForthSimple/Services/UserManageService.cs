using AutoMapper;
using ForthSimple.Identity;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class UserManageService : IUserManageService
    {
        private readonly IdentityContext dbContext;
        private readonly IMapper mapper;

        public UserManageService(IdentityContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public IEnumerable<UserVM> GetAll()
        {
            var users = dbContext.Users.AsNoTracking();
            var usersVM = mapper.Map<IEnumerable<UserVM>>(users);
            return usersVM;
        }

        public async Task<bool> BlockUsersAsync(IEnumerable<int> ids)
        {
            await dbContext.Users.Where(u => ids.Contains(u.Id)).ForEachAsync(u => u.IsBlocked = true);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnblockUsersAsync(IEnumerable<int> ids)
        {
            await dbContext.Users.Where(u => ids.Contains(u.Id)).ForEachAsync(u => u.IsBlocked = false);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUsersAsync(IEnumerable<int> ids)
        {
            dbContext.RemoveRange(dbContext.Users.Where(u => ids.Contains(u.Id)));
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}