using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
   public abstract class ProductEntity
   {
      public string Name { get; set; }
      public string Category { get; set; }
      public PriceInfo[] Prices { get; set; }
   }

   public class PriceInfo
   {
      public ShopEntity Shop { get; set; }
      public decimal Price { get; set; }
   }
}