using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(User user, List<string> roles);
    }
}