using Data.Common.Products;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongodb.Products
{
   public class MongoProductEntity : ProductEntity
   {
      public ObjectId Id { get; set; }
   }
}