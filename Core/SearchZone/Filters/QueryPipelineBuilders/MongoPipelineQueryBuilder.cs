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
   internal class MongoPipelineQueryBuilder<T> : IPipelineQueryBuilder<T> where T :IFilter
   {
      private IFilterParser<T>[] filterParsers;
      private MongoDBSettings settings;

      public MongoPipelineQueryBuilder(IOptions<MongoDBSettings> options, IEnumerable<IFilterParser<T>> parsers)
      {
         ArgumentNullException.ThrowIfNull(options, nameof(options));
         ArgumentNullException.ThrowIfNull(options.Value, nameof(options.Value));
         ArgumentNullException.ThrowIfNull(options.Value.ProductCollectionName, nameof(options.Value.ProductCollectionName));
         settings = options.Value;
         
         ArgumentNullException.ThrowIfNull(parsers, nameof(parsers));
         filterParsers = parsers.ToArray();
         if (!filterParsers.Any())
            throw new ArgumentException($"{nameof(parsers)} is empty");
      }

      public string BuildPipelineQuery(FilterPipeline<T> pipeline)
      {
         return BuildFiltersFromPipeline(pipeline);
      }

      private string BuildFiltersFromPipeline(FilterPipeline<T> pipeline)
      {
         IReadOnlyList<T> filters = pipeline.Filters;
         IFilterParser<T>? suitableParser;
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