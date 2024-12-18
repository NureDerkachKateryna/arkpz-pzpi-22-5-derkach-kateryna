using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.DbContexts;
using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContextEF _context;

        private readonly ILogger<UserRepository> _logger;

        private readonly IMapper _mapper;

        public UserRepository(DataContextEF context, ILogger<UserRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                this._context.Users.Add(user);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            try
            {
                User user = await this._context.Users.SingleAsync(x => x.Id.Equals(userId));

                this._context.Users.Remove(user);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync(Expression<Func<User, bool>> filter)
        {
            try
            {
                return await this._context.Users
                    .Include(b => b.Bans)
                        .ThenInclude(r => r.Product)
                    .Include(s => s.SkincareRoutines)
                        .ThenInclude(r => r.RoutineProducts)
                            .ThenInclude(p => p.Product)
                    .Where(filter)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> filter)
        {
            try
            {
                return await this._context.Users
                    .Include(b => b.Bans)
                        .ThenInclude(r => r.Product)
                    .Include(s => s.SkincareRoutines)
                        .ThenInclude(r => r.RoutineProducts)
                            .ThenInclude(p => p.Product)
                    .SingleAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                User dbUser = await this._context.Users.AsNoTracking().SingleAsync(u => u.Id.Equals(user.Id));

                this._mapper.Map(user, dbUser);

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
