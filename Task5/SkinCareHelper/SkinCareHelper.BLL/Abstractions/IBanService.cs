using SkinCareHelper.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Abstractions
{
    public interface IBanService
    {
        Task AddBanAsync(BanDto banDto);

        Task DeleteBanAsync(int banId);

        Task<List<BanDto>> GetAllBansAsync();

    }
}
