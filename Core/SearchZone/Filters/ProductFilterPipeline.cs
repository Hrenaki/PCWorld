using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters
{
   public class ProductFilterPipeline
   {
      private List<IProductFilter> filters = new List<IProductFilter>();
      public IReadOnlyList<IProductFilter> Filters => filters.AsReadOnly();

      public ProductFilterPipeline(params IProductFilter[] filters)
      {
         if(filters.Length < 1 && filters.Any(filter => filter == null))
            throw new ArgumentException(nameof(filters));

         this.filters.AddRange(filters);
      }

      public void AddFilter(IProductFilter filter)
      {
         if(filter == null)
            throw new ArgumentNullException(nameof(filter));

         if (!filters.Contains(filter))
            filters.Add(filter);
      }

      public void RemoveFilter(IProductFilter filter)
      {
         if(filter != null && filters.Contains(filter))
            filters.Remove(filter);
      }
   }
}