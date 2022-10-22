using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Core.OrderZone;
using Core.SearchZone;
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


         BsonSerializer.RegisterSerializer(new MongoShopEntitySerializer());
      }
   }
}