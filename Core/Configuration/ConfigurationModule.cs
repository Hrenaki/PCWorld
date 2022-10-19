using Core.OrderZone;
using Core.SearchZone;
using Core.UserZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
   public abstract class ConfigurationModule
   {
      public abstract void OnBuild(ConfigurationBuilder builder);
   }

   public sealed class CoreConfigurationModule : ConfigurationModule
   {
      public override void OnBuild(ConfigurationBuilder builder)
      {
         builder.Add<IHashService, HashService>()
                .Add<IUserService, MongoUserService>()
                .Add<IUserAuthenticationService, UserAuthenticationService>();
      }
   }
}