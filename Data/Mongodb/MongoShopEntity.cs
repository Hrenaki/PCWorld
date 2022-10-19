using Data.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongodb
{
   public class MongoShopEntity : ShopEntity
   {
      public ObjectId Id { get; set; }
   }
}