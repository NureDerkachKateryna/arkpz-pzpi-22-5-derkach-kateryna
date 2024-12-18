using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Abstractions
{
    public interface IBanRepository
    {
        Task<List<Ban>> GetAllBansAsync(Expression<Func<Ban, bool>> filter);

        Task<Ban> GetBanAsync(Expression<Func<Ban, bool>> filter);

        Task AddBanAsync(Ban ban);

        Task DeleteBanAsync(int banId);
    }
}
