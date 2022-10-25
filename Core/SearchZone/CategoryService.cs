using Core.Configuration;
using Core.SearchZone.Filters;
using Core.SearchZone.Filters.QueryPipelineBuilders;
using Data.Common;
using Data.Mongodb.Products;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone
{
   public interface ICategoryService
   {
      public List<ProductCategoryEntity> GetCategories();
      public List<ProductCategoryEntity> GetCategories(FilterPipeline<ICategoryFilter> filterPipeline);
   }

   internal class MongoCategoryService : ICategoryService
   {
      private IMongoCollection<MongoProductCategoryEntity> categories;
      private MongoDBSettings dbSettings;
      private IPipelineQueryBuilder<ICategoryFilter> queryBuilder;

      public MongoCategoryService(MongoClient client,
                                 IOptions<MongoDBSettings> settings,
                                 IPipelineQueryBuilder<ICategoryFilter> queryBuilder)
      {
         ArgumentNullException.ThrowIfNull(client, nameof(client));
         ArgumentNullException.ThrowIfNull(settings, nameof(settings));

         ArgumentNullException.ThrowIfNull(settings.Value, nameof(settings.Value));
         ArgumentNullException.ThrowIfNull(settings.Value.DatabaseName, nameof(settings.Value.DatabaseName));
         ArgumentNullException.ThrowIfNull(settings.Value.ProductCategoryCollectionName, 
                                           nameof(settings.Value.ProductCategoryCollectionName));

         ArgumentNullException.ThrowIfNull(queryBuilder, nameof(queryBuilder));

         dbSettings = settings.Value;

         categories = client.GetDatabase(dbSettings.DatabaseName)
                            .GetCollection<MongoProductCategoryEntity>(dbSettings.ProductCategoryCollectionName);

         ArgumentNullException.ThrowIfNull(categories, nameof(categories));

         this.queryBuilder = queryBuilder;
      }

      public List<ProductCategoryEntity> GetCategories()
      {
         return categories.Aggregate().ToEnumerable().Select(pce => (ProductCategoryEntity)pce).ToList();
      }

      public List<ProductCategoryEntity> GetCategories(FilterPipeline<ICategoryFilter> filterPipeline)
      {
         var queryStr = queryBuilder.BuildPipelineQuery(filterPipeline);
         var query = BsonSerializer.Deserialize<BsonDocument[]>(queryStr);

         return categories.Aggregate<MongoProductCategoryEntity>(query)
                          .ToEnumerable()
                          .Select(pce => (ProductCategoryEntity)pce)
                          .ToList();
      }
   }
}