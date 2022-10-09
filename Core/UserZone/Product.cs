using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserZone
{
   public class Product
   {
      internal ProductEntity Entity { get; init; }

      public string Name => Entity.Name;
      public decimal Price => Entity.Price;

      public CategoryInfo CategoryInfo { get; set; }

      public Product(ProductEntity entity)
      {
         Entity = entity;
         CategoryInfo = new CategoryInfo(Entity.Category);
      }
   }

   public class CategoryInfo
   {
      internal CategoryEntity Entity { get; set; }

      public string Name => Entity.Name;

      public CategoryInfo(CategoryEntity entity)
      {
         Entity = entity;
      }
   }
}
