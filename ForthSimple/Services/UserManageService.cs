using AutoMapper;
using ForthSimple.Identity;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> BlockUsersAsync(IEnumerable<string> ids)
        {
            //var users = await GetSelectedUsers(ids);
            //foreach (var user in users)
            //{
            //    user.Blocked = true;
            //}
            //await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnblockUsersAsync(IEnumerable<string> ids)
        {
            //var users = await GetSelectedUsers(ids);
            //foreach (var user in users)
            //{
            //    user.Blocked = false;
            //}
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUsersAsync(IEnumerable<string> ids)
        {
            //var users = await GetSelectedUsers(ids);
            //foreach (var user in users)
            //{
            //    dbContext.Users.Remove(user);
            //}
            await dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<List<IdentityUser>> GetSelectedUsers(IEnumerable<string> ids)
        {
            var selectedUsers = await dbContext.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
            return selectedUsers;
        }
    }
}