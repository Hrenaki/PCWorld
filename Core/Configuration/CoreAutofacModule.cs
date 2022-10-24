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

         builder.RegisterType<MongoUserService>().As<IUserService>();
         builder.RegisterType<MongoUserRoleService>().As<IUserRoleService>();
         builder.RegisterType<MongoProductService>().As<IProductService>();
         
         builder.RegisterType<UserAuthenticationService>().As<IUserAuthenticationService>();

         builder.RegisterType<MongoPipelineQueryBuilder>().As<IPipelineQueryBuilder>();
         builder.RegisterType<MongoProductPriceFilterParser>().As<IProductFilterParser>();
         builder.RegisterType<MongoProductCategoryFilterParser>().As<IProductFilterParser>();

         BsonSerializer.RegisterSerializer(new MongoShopEntitySerializer());
      }
   }
}