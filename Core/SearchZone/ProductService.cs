using Core.UserZone;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Common;

namespace Core.SearchZone
{
   public interface IProductService
   {
      public List<ProductEntity> GetProducts();
      public List<ProductEntity> GetProducts(ProductFilter filter);
   }

   internal class EfProductService : IProductService
   {
      private readonly MainDbContext dbContext;

      public EfProductService(MainDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      public List<ProductEntity> GetProducts()
      {
         return null;
         //return dbContext.Products.Include(p => p.Category)
         //                         .Select(pe => new Product(pe))
         //                         .ToList();
      }

      public List<ProductEntity> GetProducts(ProductFilter filter)
      {
         return null;
         //return dbContext.Products.Include(pe => pe.Category)
         //                         .AsEnumerable()
         //                         .Where(pe => filter.GetResult(pe))
         //                         .Select(pe => new Product(pe))
         //                         .ToList();
      }
   }
}