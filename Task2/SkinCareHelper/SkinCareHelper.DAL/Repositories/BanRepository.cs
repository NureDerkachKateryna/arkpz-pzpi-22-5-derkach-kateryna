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
    public class BanRepository : IBanRepository
    {
        private readonly DataContextEF _context;

        private readonly ILogger<BanRepository> _logger;

        private readonly IMapper _mapper;

        public BanRepository(DataContextEF context, ILogger<BanRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddBanAsync(Ban ban)
        {
            try
            {
                this._context.Bans.Add(ban);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteBanAsync(int banId)
        {
            try
            {
                Ban ban = await this._context.Bans.SingleAsync(x => x.BanId == banId);

                this._context.Bans.Remove(ban);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Ban>> GetAllBansAsync(Expression<Func<Ban, bool>> filter)
        {
            try
            {
                return await this._context.Bans
                    .Include(b => b.User)
                    .Include(b => b.Product)
                    .Where(filter)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Ban> GetBanAsync(Expression<Func<Ban, bool>> filter)
        {
            try
            {
                return await this._context.Bans
                    .Include(b => b.User)
                    .Include(b => b.Product)
                    .SingleAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
