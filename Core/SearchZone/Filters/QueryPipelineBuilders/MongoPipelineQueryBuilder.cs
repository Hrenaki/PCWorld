using Core.Configuration;
using Core.SearchZone.Filters.Parsers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.QueryPipelineBuilders
{
   internal class MongoPipelineQueryBuilder : IPipelineQueryBuilder
   {
      private IProductFilterParser[] filterParsers;
      private MongoDBSettings settings;

      public MongoPipelineQueryBuilder(IOptions<MongoDBSettings> options, IEnumerable<IProductFilterParser> parsers)
      {
         ArgumentNullException.ThrowIfNull(options, nameof(options));
         ArgumentNullException.ThrowIfNull(options.Value, nameof(options.Value));
         ArgumentNullException.ThrowIfNull(options.Value.ProductCollectionName, nameof(options.Value.ProductCollectionName));
         settings = options.Value;
         
         ArgumentNullException.ThrowIfNull(parsers, nameof(parsers));
         filterParsers = parsers.ToArray();
      }

      public string BuildPipelineQuery(ProductFilterPipeline pipeline)
      {
         return BuildFiltersFromPipeline(pipeline);
      }

      private string BuildFiltersFromPipeline(ProductFilterPipeline pipeline)
      {
         IReadOnlyList<IProductFilter> filters = pipeline.Filters;
         IProductFilterParser? suitableParser;
         List<string> parsedFilters = new List<string>();

         foreach (var filter in filters)
         {
            suitableParser = filterParsers.FirstOrDefault(parser => parser.CanParse(filter));
            if (suitableParser == null)
               continue;

            parsedFilters.Add(suitableParser.Parse(filter));
         }

         StringBuilder sb = new StringBuilder("[");
         sb.AppendJoin(", ", parsedFilters);
         sb.Append("]");
         return sb.ToString();
      }
   }
}