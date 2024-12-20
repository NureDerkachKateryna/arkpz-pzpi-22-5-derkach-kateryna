using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Abstractions
{
    public interface ISkincareRoutineRepository
    {
        Task<List<SkincareRoutine>> GetAllSkincareRoutinesAsync(Expression<Func<SkincareRoutine, bool>> filter);

        Task<SkincareRoutine> GetSkincareRoutineAsync(Expression<Func<SkincareRoutine, bool>> filter);

        Task AddSkincareRoutineAsync(SkincareRoutine skincareRoutine);

        Task UpdateSkincareRoutineAsync(SkincareRoutine skincareRoutine);

        Task DeleteSkincareRoutineAsync(int skincareRoutineId);

        Task AddProductToSkincareRoutineAsync(int skincareRoutineId, int productId);

        Task DeleteProductFromSkincareRoutineAsync(int skincareRoutineId, int productId);

        Task<List<SkincareRoutine>> GenerateSkincareRoutinesAsync(List<Product> userProducts, string userId);

        Task AddOrReplaceProductInRoutinesAsync(string userId, Product newProduct, List<Product> usersProducts);

        Task RemoveProductFromRoutinesAsync(string userId, int productId);

    }
}
