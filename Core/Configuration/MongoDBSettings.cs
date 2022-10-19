using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
   public class MongoDBSettings
   {
      public string ConnectionString { get; set; }
      
      public string DatabaseName { get; set; }

      public string UserCollectionName { get; set; }
      public string UserRoleCollectionName { get; set; }

      public string ProductCollectionName { get; set; }
      public string ShopCollectionName { get; set; }
   }
}