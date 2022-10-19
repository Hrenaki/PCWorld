using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEFtest.Mongo
{
   public class MongoUserEntity : UserEntity
   {
      public ObjectId Id { get; set; }
      public override string Name { get; set; }
      public override string Hash { get; set; }
   }
}