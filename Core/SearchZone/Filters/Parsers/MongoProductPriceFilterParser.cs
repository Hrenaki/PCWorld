using Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.Parsers
{
   internal class MongoProductPriceFilterParser : IProductFilterParser
   {
      private static readonly string format = "{{$match: {{Category: {{$gte: {0}, $lte: {1} }}}}}}";
      public bool CanParse(IProductFilter filter) => filter is ProductPriceFilter;

      public string Parse(IProductFilter filter)
      {
         ProductPriceFilter? priceFilter = filter as ProductPriceFilter;
         if (priceFilter == null)
            throw new ArgumentNullException($"{nameof(filter)} must be a type of {nameof(ProductPriceFilter)}");

         return string.Format(format, priceFilter.MinPrice, priceFilter.MaxPrice);
      }
   }
}