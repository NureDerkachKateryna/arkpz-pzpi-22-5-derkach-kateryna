using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.DbContexts;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Repositories
{
    public class SkincareRoutineRepository : ISkincareRoutineRepository
    {
        private readonly DataContextEF _context;

        private readonly ILogger<SkincareRoutineRepository> _logger;

        private readonly IMapper _mapper;


        public SkincareRoutineRepository(DataContextEF context, ILogger<SkincareRoutineRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddProductToSkincareRoutineAsync(int skincareRoutineId, int productId)
        {
            try
            {
                this._context.RoutineProducts.Add(new RoutineProduct { ProductId = productId, SkincareRoutineId = skincareRoutineId });
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task AddSkincareRoutineAsync(SkincareRoutine skincareRoutine)
        {
            try
            {
                this._context.SkincareRoutines.Add(skincareRoutine);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteProductFromSkincareRoutineAsync(int skincareRoutineId, int productId)
        {
            try
            {
                RoutineProduct routineProduct = await this._context.RoutineProducts.SingleAsync(x => x.SkincareRoutineId == skincareRoutineId && x.ProductId == productId);

                this._context.RoutineProducts.Remove(routineProduct);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteSkincareRoutineAsync(int skincareRoutineId)
        {
            try
            {
                SkincareRoutine skincareRoutine = await this._context.SkincareRoutines.SingleAsync(x => x.SkincareRoutineId == skincareRoutineId);

                this._context.SkincareRoutines.Remove(skincareRoutine);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<SkincareRoutine>> GetAllSkincareRoutinesAsync(Expression<Func<SkincareRoutine, bool>> filter)
        {
            try
            {
                return await this._context.SkincareRoutines
                    .Include(s => s.User)
                    .Include(s => s.RoutineProducts)
                        .ThenInclude(r => r.Product)
                    .Where(filter)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SkincareRoutine> GetSkincareRoutineAsync(Expression<Func<SkincareRoutine, bool>> filter)
        {
            try
            {
                return await this._context.SkincareRoutines
                    .Include(s => s.User)
                    .Include(s => s.RoutineProducts)
                        .ThenInclude(r => r.Product)
                    .SingleAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateSkincareRoutineAsync(SkincareRoutine skincareRoutine)
        {
            try
            {
                SkincareRoutine dbSkincareRoutine = await this._context.SkincareRoutines.AsNoTracking().Include(s => s.RoutineProducts).SingleAsync(s => s.SkincareRoutineId == skincareRoutine.SkincareRoutineId);

                this._mapper.Map(skincareRoutine, dbSkincareRoutine);

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<SkincareRoutine>> GenerateSkincareRoutinesAsync(List<Product> userProducts, string userId)
        {
            try
            {
                var routines = new List<SkincareRoutine>();
                var daysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();

                var morningProducts = userProducts
                    .Where(p => p.ProductType != ProductType.cleansing_oil &&
                                p.ProductType != ProductType.eye_cream &&
                                p.ProductType != ProductType.acid &&
                                p.ProductType != ProductType.retinol &&
                                p.ProductType != ProductType.exfoliation)
                    .ToList();

                var eveningProducts = userProducts
                    .Where(p => p.ProductType != ProductType.sunscreen &&
                                p.ProductType != ProductType.exfoliation)
                    .ToList();

                var exfoliationProduct = userProducts.FirstOrDefault(p => p.ProductType == ProductType.exfoliation);
                var acidProduct = userProducts.FirstOrDefault(p => p.ProductType == ProductType.acid);
                var retinolProduct = userProducts.FirstOrDefault(p => p.ProductType == ProductType.retinol);

                foreach (var day in daysOfWeek)
                {
                    var morningRoutine = new SkincareRoutine
                    {
                        CreationDate = DateTime.Now,
                        DayOfWeek = day,
                        TimeOfDay = TimeOfDay.Morning,
                        RoutineProducts = new List<RoutineProduct>()
                    };

                    if (day == DayOfWeek.Wednesday)
                    {
                        if (exfoliationProduct != null)
                        {
                            morningRoutine.RoutineProducts.Add(new RoutineProduct { ProductId = exfoliationProduct.ProductId });
                        }
                    }

                    morningRoutine.RoutineProducts.AddRange(morningProducts.Select(p => new RoutineProduct { ProductId = p.ProductId }));

                    routines.Add(morningRoutine);

                    var eveningRoutine = new SkincareRoutine
                    {
                        CreationDate = DateTime.Now,
                        DayOfWeek = day,
                        TimeOfDay = TimeOfDay.Evening,
                        RoutineProducts = new List<RoutineProduct>()
                    };

                    eveningRoutine.RoutineProducts.AddRange(eveningProducts.Select(p => new RoutineProduct { ProductId = p.ProductId }));

                    if (day == DayOfWeek.Tuesday || day == DayOfWeek.Friday)
                    {
                        if (retinolProduct != null)
                        {
                            eveningRoutine.RoutineProducts.Add(new RoutineProduct { ProductId = retinolProduct.ProductId });
                        }
                    }
                    else if (day == DayOfWeek.Wednesday || day == DayOfWeek.Saturday)
                    {
                        if (acidProduct != null)
                        {
                            eveningRoutine.RoutineProducts.Add(new RoutineProduct { ProductId = acidProduct.ProductId });
                        }
                    }

                    routines.Add(eveningRoutine);
                }

                foreach (var routine in routines)
                {
                    routine.UserId = userId;
                    _context.SkincareRoutines.Add(routine);

                    foreach (var routineProduct in routine.RoutineProducts)
                    {
                        routineProduct.SkincareRoutineId = routine.SkincareRoutineId;
                        _context.RoutineProducts.Add(routineProduct);
                    }
                }

                await this._context.SaveChangesAsync();

                return routines;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private bool ShouldAddProductToRoutine(Product product, TimeOfDay timeOfDay, DayOfWeek dayOfWeek)
        {
            if (product.ProductType == ProductType.sunscreen && timeOfDay == TimeOfDay.Morning)
                return true;

            if (product.ProductType == ProductType.exfoliation && dayOfWeek == DayOfWeek.Wednesday && timeOfDay == TimeOfDay.Morning)
                return true;

            if (product.ProductType == ProductType.retinol &&
                (dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Friday) &&
                timeOfDay == TimeOfDay.Evening)
                return true;

            if (product.ProductType == ProductType.acid &&
                (dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Saturday) &&
                timeOfDay == TimeOfDay.Evening)
                return true;

            if ((product.ProductType == ProductType.eye_cream || product.ProductType == ProductType.cleansing_oil)
                && timeOfDay == TimeOfDay.Evening)
                return true;

            if (product.ProductType == ProductType.toner || 
                product.ProductType == ProductType.serum ||
                product.ProductType == ProductType.cleanser ||
                product.ProductType == ProductType.moisturizer)
                return true;

            return false;
        }

        public async Task AddOrReplaceProductInRoutinesAsync(string userId, Product newProduct, List<Product> usersProducts)
        {
            try
            {
                var userRoutines = await _context.SkincareRoutines
                    .Include(r => r.RoutineProducts)
                    .Where(r => r.UserId == userId)
                    .ToListAsync();

                Product existingProduct = null!;

                foreach (var product in usersProducts)
                {
                    if (product.ProductType == newProduct.ProductType)
                    {
                        existingProduct = product;
                    }
                }

                if (existingProduct != null)
                {
                    foreach (var routine in userRoutines)
                    {
                        foreach (var routineProduct in routine.RoutineProducts
                                     .Where(rp => rp.ProductId == existingProduct.ProductId))
                        {
                            routineProduct.ProductId = newProduct.ProductId;
                        }
                    }
                }
                else
                {
                    foreach (var routine in userRoutines)
                    {
                        if (ShouldAddProductToRoutine(newProduct, routine.TimeOfDay, routine.DayOfWeek))
                        {
                            routine.RoutineProducts.Add(new RoutineProduct
                            {
                                ProductId = newProduct.ProductId,
                                SkincareRoutineId = routine.SkincareRoutineId
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task RemoveProductFromRoutinesAsync(string userId, int productId)
        {
            try
            {
                var userRoutines = await _context.SkincareRoutines
                    .Include(r => r.RoutineProducts)
                    .Where(r => r.UserId == userId)
                    .ToListAsync();

                foreach (var routine in userRoutines)
                {
                    routine.RoutineProducts.RemoveAll(rp => rp.ProductId == productId);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
