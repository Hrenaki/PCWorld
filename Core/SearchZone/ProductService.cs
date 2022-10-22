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

namespace Core.SearchZone
{
   public interface IProductService
   {
      public List<ProductEntity> GetProducts();
      public List<ProductEntity> GetProducts(ProductFilter filter);
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

      public List<ProductEntity> GetProducts(ProductFilter filter)
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

      public MongoProductService(MongoClient client, IOptions<MongoDBSettings> settings)
      {
         ArgumentNullException.ThrowIfNull(client, nameof(client));
         ArgumentNullException.ThrowIfNull(settings, nameof(settings));

         ArgumentNullException.ThrowIfNull(settings.Value, nameof(settings.Value));
         ArgumentNullException.ThrowIfNull(settings.Value.DatabaseName, nameof(settings.Value.DatabaseName));
         ArgumentNullException.ThrowIfNull(settings.Value.ProductCollectionName, nameof(settings.Value.ProductCollectionName));

         dbSettings = settings.Value;

         products = client.GetDatabase(dbSettings.DatabaseName)
                          .GetCollection<MongoProductEntity>(dbSettings.ProductCollectionName);
         
         ArgumentNullException.ThrowIfNull(products, nameof(products));
      }

      public List<ProductEntity> GetProducts()
      {
         var res = products.Aggregate().Unwind("Prices")
                                    .Lookup(dbSettings.ShopCollectionName, "Prices.ShopId", "_id", "Prices.Shop")
                                    .Unwind("Prices.Shop")
                                    .Project(new BsonDocument("Prices.ShopId", "0"))
                                    .Group(new BsonDocument() { { "_id", "$_id"},
                                                                { "root", new BsonDocument("$mergeObjects", "$$ROOT")},
                                                                { "Prices", new BsonDocument("$push", "$Prices")} })
                                    .ReplaceRoot(u => new BsonDocument() { { "$mergeObjects", new BsonArray(new[] { "$root", "$$ROOT" }) } })
                                    .Project<MongoProductEntity>(new BsonDocument()
                                    {
                                       { "root", 0},
                                       { "Specifications", 0 }
                                    });

         return products.Aggregate().Unwind("Prices")
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
                                    })
                                    .ToEnumerable()
                                    .Select(pe => (ProductEntity)pe)
                                    .ToList();
      }

      public List<ProductEntity> GetProducts(ProductFilter filter)
      {
         throw new NotImplementedException();
      }
   }
}