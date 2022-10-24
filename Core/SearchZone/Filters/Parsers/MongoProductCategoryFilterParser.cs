using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.Parsers
{
   internal class MongoProductCategoryFilterParser : IProductFilterParser
   {
      private static readonly string format = "{{$match: {{Category: \"{0}\" }}}}";
      public bool CanParse(IProductFilter filter) => filter is ProductCategoryFilter;

      public string Parse(IProductFilter filter)
      {
         ProductCategoryFilter? categoryFilter = filter as ProductCategoryFilter;
         if (categoryFilter == null)
            throw new ArgumentException($"{nameof(filter)} must be a type of {nameof(ProductCategoryFilter)}");

         return string.Format(format, categoryFilter.CategoryName);
      }
   }
}