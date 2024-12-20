using Microsoft.AspNetCore.Identity;
using SkinCareHelper.DAL.DbContexts;
using SkinCareHelper.DAL.Entities;

namespace SkinCareHelper.DAL.Repositories
{
    public class Seed
    {
        public static async Task SeedData(DataContextEF context, UserManager<User> userManager)
        {
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "ee80df5e-b172-4943-b968-643b028f1b7d", Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "CUSTOMER" },
            };

            context.Roles.AddRange(roles);

            await context.SaveChangesAsync();
        }
    }
}
