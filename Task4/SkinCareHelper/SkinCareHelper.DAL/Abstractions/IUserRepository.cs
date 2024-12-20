using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(Expression<Func<User, bool>> filter);

        Task<User> GetUserAsync(Expression<Func<User, bool>> filter);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(string userId);
    }
}
