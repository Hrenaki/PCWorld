using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Core.OrderZone;
using Core.SearchZone;
using Core.SearchZone.Filters;
using Core.SearchZone.Filters.Parsers;
using Core.SearchZone.Filters.QueryPipelineBuilders;
using Core.UserZone;
using Data.Mongodb.Serializers;
using MongoDB.Bson.Serialization;

namespace Core.Configuration
{
   public class CoreAutofacModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterType<HashService>().As<IHashService>().SingleInstance();

         // Mongo services
         builder.RegisterType<MongoUserService>().As<IUserService>();
         builder.RegisterType<MongoUserRoleService>().As<IUserRoleService>();
         builder.RegisterType<MongoProductService>().As<IProductService>();
         
         // Common services
         builder.RegisterType<UserAuthenticationService>().As<IUserAuthenticationService>();

         // Filter pipelines
         builder.RegisterType<MongoPipelineQueryBuilder<IProductFilter>>().As<IPipelineQueryBuilder<IProductFilter>>();
         builder.RegisterType<MongoPipelineQueryBuilder<ICategoryFilter>>().As<IPipelineQueryBuilder<ICategoryFilter>>();

         // Filter parsers
         builder.RegisterType<MongoProductPriceFilterParser>().AsImplementedInterfaces();
         builder.RegisterType<MongoProductCategoryFilterParser>().AsImplementedInterfaces();
         builder.RegisterType<MongoCategoryNameFilterParser>().AsImplementedInterfaces();

         // Mongo serializers
         BsonSerializer.RegisterSerializer(new MongoShopEntitySerializer());
         BsonSerializer.RegisterSerializer(new MongoProductCategoryEntitySerializer());
      }
   }
}