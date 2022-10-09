using Core.UserZone;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Configuration;

namespace Desktop.Configuration
{
   internal sealed class PCWorldContext : MainDbContext
   {
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=pcworld;Trusted_Connection=True;");
      }
   }

   public class DesktopConfigurationModule : ConfigurationModule
   {
      public override void OnBuild(ConfigurationBuilder builder)
      {
         builder.AddModule<CoreConfigurationModule>()
                .Add<MainDbContext, PCWorldContext>();
      }
   }
}