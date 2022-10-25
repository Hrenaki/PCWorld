using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.QueryPipelineBuilders
{
   public interface IPipelineQueryBuilder<T> where T : IFilter
   {
      public string BuildPipelineQuery(FilterPipeline<T> pipeline);
   }
}