using SkinCareHelper.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Abstractions
{
    public interface ISkincareRoutineService
    {
        Task AddSkincareRoutineAsync(SkincareRoutineDto skincareRoutineDto);

        Task UpdateSkincareRoutineAsync(SkincareRoutineDto skincareRoutineDto);

        Task DeleteSkincareRoutineAsync(int skincareRoutineId);

        Task<SkincareRoutineDto> GetSkincareRoutineByIdAsync(int skincareRoutineId);

        Task<List<SkincareRoutineDto>> GetAllSkincareRoutinesAsync();

        Task<List<SkincareRoutineDto>> GetSkincareRoutinesByUserAsync(string userId);

        Task AddProductToSkincareRoutineAsync(int skincareRoutineId, int productId);

        Task<List<SkincareRoutineDto>> GenerateUserRoutinesAsync(string userId);

        Task AddOrReplaceProductInRoutinesAsync(string userId, int newProductId);

        Task RemoveProductFromRoutinesAsync(string userId, int productId);
    }
}
