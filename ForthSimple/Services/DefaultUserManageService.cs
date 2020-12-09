using AutoMapper;
using ForthSimple.Interfaces;
using ForthSimple.Models;
using ForthSimple.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class DefaultUserManageService : IUserManageService
    {
        private readonly ForthDbContext dbContext;

        private readonly IMapper mapper;

        public DefaultUserManageService(ForthDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Task<bool> BlockUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BlockUsersAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserVM> GetAll()
        {
            var users = dbContext.Users.AsNoTracking();
            var usersVM = mapper.Map<IEnumerable<UserVM>>(users);
            return usersVM;
        }

        public Task<bool> UnblockUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnblockUsersAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
