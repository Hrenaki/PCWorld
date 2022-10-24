using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters
{
   public sealed class ProductPriceFilter : IProductFilter
   {
      public decimal MinPrice { get; set; }
      public decimal MaxPrice { get; set; }
   }
}