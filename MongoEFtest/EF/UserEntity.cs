using MongoDB.Bson;
using MongoEFtest.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEFtest.EF
{
   public class UserEfEntity : UserEntity
   {
      public int Id { get; set; }
      public override string Name { get; set; }
      public override string Hash { get; set; }
   }
}