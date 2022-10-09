using Core.UserZone;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone
{
   public interface IProductService
   {
      public List<Product> GetProducts();
      public List<Product> GetProducts(ProductFilter filter);
   }

   internal class ProductService : IProductService
   {
      private readonly MainDbContext dbContext;

      public ProductService(MainDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      public List<Product> GetProducts()
      {
         return dbContext.Products.Include(p => p.Category)
                                  .Select(pe => new Product(pe))
                                  .ToList();
      }

      public List<Product> GetProducts(ProductFilter filter)
      {
         return dbContext.Products.Include(pe => pe.Category)
                                  .AsEnumerable()
                                  .Where(pe => filter.GetResult(pe))
                                  .Select(pe => new Product(pe))
                                  .ToList();
      }
   }
}