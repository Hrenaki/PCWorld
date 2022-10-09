using Core.UserZone;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Data
{
	public class MainDbContext : DbContext
	{
		public DbSet<UserEntity> Users { get; set; }
		public DbSet<UserRoleEntity> UserRoles { get; set; }

      public DbSet<ProductEntity> Products { get; set; }
      public DbSet<CategoryEntity> Categories { get; set; }
      public DbSet<BasketItemEntity> UserProducts { get; set; }

      public DbSet<OrderEntity> Orders { get; set; }
      public DbSet<OrderStatusEntity> OrderStatuses { get; set; }
      public DbSet<OrderProductEntity> OrderProducts { get; set; }

      public MainDbContext() { }

      public MainDbContext(DbContextOptions options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.Entity<BasketItemEntity>(t =>
         {
            t.HasKey(bItem => new { bItem.UserId, bItem.ProductId });
         });

         builder.Entity<OrderProductEntity>(t =>
         {
            t.HasKey(item => new { item.OrderId, item.ProductId });
         });
      }
   }
}