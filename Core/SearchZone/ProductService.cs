using Core.UserZone;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Common;
using MongoDB.Driver;
using Data.Mongodb;
using Microsoft.Extensions.Options;
using Core.Configuration;
using MongoDB.Bson;
using Core.SearchZone.Filters;
using Core.SearchZone.Filters.QueryPipelineBuilders;
using MongoDB.Bson.Serialization;

namespace Core.SearchZone
{
   public interface IProductService
   {
      public List<ProductEntity> GetProducts();
      public List<ProductEntity> GetProducts(ProductFilterPipeline filter);
   }

   internal class EfProductService : IProductService
   {
      private readonly MainDbContext dbContext;

      public EfProductService(MainDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      public List<ProductEntity> GetProducts()
      {
         return null;
         //return dbContext.Products.Include(p => p.Category)
         //                         .Select(pe => new Product(pe))
         //                         .ToList();
      }

      public List<ProductEntity> GetProducts(ProductFilterPipeline filter)
      {
         return null;
         //return dbContext.Products.Include(pe => pe.Category)
         //                         .AsEnumerable()
         //                         .Where(pe => filter.GetResult(pe))
         //                         .Select(pe => new Product(pe))
         //                         .ToList();
      }
   }

   internal class MongoProductService : IProductService
   {
      private IMongoCollection<MongoProductEntity> products;
      private MongoDBSettings dbSettings;
      private IPipelineQueryBuilder queryBuilder;

      private BsonDocument[] includeShopsBson;

      public MongoProductService(MongoClient client,
                                 IOptions<MongoDBSettings> settings,
                                 IPipelineQueryBuilder queryBuilder)
      {
         ArgumentNullException.ThrowIfNull(client, nameof(client));
         ArgumentNullException.ThrowIfNull(settings, nameof(settings));

         ArgumentNullException.ThrowIfNull(settings.Value, nameof(settings.Value));
         ArgumentNullException.ThrowIfNull(settings.Value.DatabaseName, nameof(settings.Value.DatabaseName));
         ArgumentNullException.ThrowIfNull(settings.Value.ProductCollectionName, nameof(settings.Value.ProductCollectionName));

         ArgumentNullException.ThrowIfNull(queryBuilder, nameof(queryBuilder));

         dbSettings = settings.Value;

         products = client.GetDatabase(dbSettings.DatabaseName)
                          .GetCollection<MongoProductEntity>(dbSettings.ProductCollectionName);
         
         ArgumentNullException.ThrowIfNull(products, nameof(products));

         this.queryBuilder = queryBuilder;

         SetIncludeShopsBson();
      }

      private void SetIncludeShopsBson()
      {
         includeShopsBson = new BsonDocument[]
         {
            new BsonDocument("$unwind", "$Prices"),
            new BsonDocument("$lookup", new BsonDocument()
            {
               { "from", dbSettings.ShopCollectionName },
               { "localField", "Prices.ShopId" },
               { "foreignField", "_id" },
               { "as", "Prices.Shop" }
            }),
            new BsonDocument("$unwind", "$Prices.Shop"),
            new BsonDocument("$group", new BsonDocument()
            {
               { "_id", "$_id" },
               { "root", new BsonDocument("$mergeObjects", "$$ROOT") },
               { "Prices", new BsonDocument("$push", "$Prices") }
            }),
            new BsonDocument("$replaceRoot", new BsonDocument("newRoot", new BsonDocument("$mergeObjects", new BsonArray(new[] { "$root", "$$ROOT" })))),
            new BsonDocument("$project", new BsonDocument() {
               { "root", 0},
               { "Specifications", 0 },
               { "Prices.ShopId", 0 }
            })
         };
      }

      public List<ProductEntity> GetProducts()
      {
         return products.Aggregate<MongoProductEntity>(includeShopsBson)
                        .ToEnumerable()
                        .Select(pe => (ProductEntity)pe)
                        .ToList();
      }

      private IAggregateFluent<MongoProductEntity> IncludeShops(IAggregateFluent<MongoProductEntity> products)
      {
         return products.Unwind("Prices")
                        .Lookup(dbSettings.ShopCollectionName, "Prices.ShopId", "_id", "Prices.Shop")
                        .Unwind("Prices.Shop")
                        .Group(new BsonDocument() { { "_id", "$_id"},
                                                    { "root", new BsonDocument("$mergeObjects", "$$ROOT")},
                                                    { "Prices", new BsonDocument("$push", "$Prices")} })
                        .ReplaceRoot(u => new BsonDocument() { { "$mergeObjects", new BsonArray(new[] { "$root", "$$ROOT" }) } })
                        .Project<MongoProductEntity>(new BsonDocument()
                        {
                           { "root", 0},
                           { "Specifications", 0 },
                           { "Prices.ShopId", 0 }
                        });
      }

      public List<ProductEntity> GetProducts(ProductFilterPipeline filterPipeline)
      {
         var queryStr = queryBuilder.BuildPipelineQuery(filterPipeline);
         var query = BsonSerializer.Deserialize<BsonDocument[]>(queryStr).ToList();
         query.AddRange(includeShopsBson);

         return products.Aggregate<MongoProductEntity>(query)
                        .ToEnumerable()
                        .Select(pe => (ProductEntity)pe)
                        .ToList();
      }
   }
}