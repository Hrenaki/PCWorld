using Core.UserZone;
using Data.EntityFramework;
using Data.EntityFramework.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone
{
    public class ProductFilter
   {
      protected Func<EfProductEntity, bool> filter;

      internal ProductFilter(Func<EfProductEntity, bool> filter)
      {
         this.filter = filter;
      }

      internal bool GetResult(EfProductEntity product) => filter(product);

      public ProductFilter And(ProductFilter other)
      {
         return new ProductFilter(p => filter(p) && other.filter(p));
      }

      public ProductFilter Or(ProductFilter other)
      {
         return new ProductFilter(p => filter(p) || other.filter(p));
      }

      public ProductFilter Not()
      {
         return new ProductFilter(p => !filter(p));
      }
   }

   public class PriceProductFilter : ProductFilter
   {
      public decimal MinPrice{ get; init; }
      public decimal MaxPrice{ get; init; }

      public PriceProductFilter(decimal minPrice, decimal maxPrice) : 
         base(p => true)
      {
         
      }
   }

   public class CategoryFilter : ProductFilter
   {
      public CategoryFilter(params string[] categoryNames) : 
         base(p => true)
      {
      }
   }

   public class NameFilter : ProductFilter
   {
      public NameFilter(string name) : 
         base(pe =>
         {
            var peName = pe.Name.ToLower();
            var lowerName = name.ToLower();
            return peName.Contains(lowerName);
         })
      {
      }
   }
}