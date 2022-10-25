using Core.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.Parsers
{
   internal class MongoCategoryNameFilterParser : ICategoryFilterParser
   {
      private static readonly string format = "{{$match: {{Name: {0} }}}}";

      public bool CanParse(ICategoryFilter filter) => filter is CategoryFilter;

      public string Parse(ICategoryFilter filter)
      {
         CategoryFilter? categoryFilter = filter as CategoryFilter;
         if (categoryFilter == null)
            throw new InvalidFilterTypeException(nameof(ICategoryFilter), nameof(CategoryFilter));

         return string.Format(format, categoryFilter.CategoryName);
      }
   }
}