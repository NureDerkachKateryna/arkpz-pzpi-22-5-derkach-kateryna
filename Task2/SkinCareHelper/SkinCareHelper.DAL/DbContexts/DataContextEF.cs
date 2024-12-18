using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkinCareHelper.DAL.Entities;

namespace SkinCareHelper.DAL.DbContexts
{
    public class DataContextEF : IdentityDbContext<User>
    {        
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Ban> Bans { get; set; }

        public virtual DbSet<SkincareRoutine> SkincareRoutines { get; set; }

        public virtual DbSet<RoutineProduct> RoutineProducts { get; set; }

        public virtual DbSet<Photo> Photos { get; set; }

        public DataContextEF() { }

        public DataContextEF(DbContextOptions<DataContextEF> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }
    }
}
