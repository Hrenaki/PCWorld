using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters
{
   public class FilterPipeline<T> where T : IFilter
   {
      private List<T> filters = new List<T>();
      public IReadOnlyList<T> Filters => filters.AsReadOnly();

      public FilterPipeline(params T[] filters)
      {
         if(filters.Length < 1 && filters.Any(filter => filter == null))
            throw new ArgumentException(nameof(filters));

         this.filters.AddRange(filters);
      }

      public void AddFilter(T filter)
      {
         if(filter == null)
            throw new ArgumentNullException(nameof(filter));

         if (!filters.Contains(filter))
            filters.Add(filter);
      }

      public void RemoveFilter(T filter)
      {
         if(filter != null && filters.Contains(filter))
            filters.Remove(filter);
      }
   }
}