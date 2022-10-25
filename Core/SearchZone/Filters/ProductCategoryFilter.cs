using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters
{
   public sealed class ProductCategoryFilter : IProductFilter
   {
      public string CategoryName { get; set; }
   }
}