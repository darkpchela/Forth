using AutoMapper;
using ForthSimple.Identity;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class UserManageService : IUserManageService
    {
        private readonly IdentityContext dbContext;
        private readonly IMapper mapper;
        private readonly IIdentityUnitOfWork identityUnitOfWork;
        private readonly Claim BlockingClaim = new Claim("IsBlocked", "true");

        public UserManageService(IdentityContext dbContext, IMapper mapper, IIdentityUnitOfWork identityUnitOfWork)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.identityUnitOfWork = identityUnitOfWork;
        }

        public IEnumerable<UserVM> GetAll()
        {
            var users = dbContext.Users.AsNoTracking();
            var usersVM = mapper.Map<IEnumerable<UserVM>>(users);
            return usersVM;
        }

        public async Task<bool> BlockUsersAsync(IEnumerable<string> ids)
        {
            var users = await GetSelectedUsers(ids);
            foreach (var user in users)
            {
                await identityUnitOfWork.UserManager.AddClaimAsync(user, BlockingClaim);
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnblockUsersAsync(IEnumerable<string> ids)
        {
            var users = await GetSelectedUsers(ids);
            foreach (var user in users)
            {
               var res = await identityUnitOfWork.UserManager.RemoveClaimAsync(user, BlockingClaim);
                if (!res.Succeeded)
                    return false;
            }
            return true;
        }

        public async Task<bool> DeleteUsersAsync(IEnumerable<string> ids)
        {
            var users = await GetSelectedUsers(ids);
            foreach (var user in users)
            {
            }
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