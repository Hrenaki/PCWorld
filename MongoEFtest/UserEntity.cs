using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEFtest
{
   public abstract class UserEntity
   {
      public abstract string Name { get; set; }
      public abstract string Hash { get; set; }
   }
}