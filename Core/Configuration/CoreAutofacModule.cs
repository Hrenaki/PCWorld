using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Core.OrderZone;
using Core.UserZone;

namespace Core.Configuration
{
   public class CoreAutofacModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterType<HashService>().As<IHashService>().SingleInstance();
         builder.RegisterType<UserService>().As<IUserService>();
         builder.RegisterType<UserAuthenticationService>().As<IUserAuthenticationService>();
         builder.RegisterType<BasketService>().As<IBasketService>();
         builder.RegisterType<OrderService>().As<IOrderService>();
      }
   }
}