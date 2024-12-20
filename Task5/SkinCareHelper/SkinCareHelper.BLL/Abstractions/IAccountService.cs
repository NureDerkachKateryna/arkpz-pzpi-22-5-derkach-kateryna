using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions
{
    public interface IAccountService
    {
        Task<string> LoginAsync(string username, string password);

        Task<bool> RegistrateAsync(User user, string password);

        Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword);
    }
}