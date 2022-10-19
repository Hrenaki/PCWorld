using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data.EntityFramework.Products;

namespace Data.EntityFramework
{
    public class MainDbContext : DbContext
    {
        public DbSet<EfUserEntity> Users { get; set; }
        public DbSet<EfUserRoleEntity> UserRoles { get; set; }

        public DbSet<EfProductEntity> Products { get; set; }
        public DbSet<EfCategoryEntity> Categories { get; set; }
        public DbSet<EfBasketItemEntity> UserProducts { get; set; }

        public DbSet<EfOrderEntity> Orders { get; set; }
        public DbSet<EfOrderStatusEntity> OrderStatuses { get; set; }
        public DbSet<EfOrderProductEntity> OrderProducts { get; set; }

        public MainDbContext() { }

        public MainDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EfBasketItemEntity>(t =>
            {
                t.HasKey(bItem => new { bItem.UserId, bItem.ProductId });
            });

            builder.Entity<EfOrderProductEntity>(t =>
            {
                t.HasKey(item => new { item.OrderId, item.ProductId });
            });
        }
    }
}