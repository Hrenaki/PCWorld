using Data.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongodb
{
   public class MongoUserRoleEntity : UserRoleEntity
   {
      public ObjectId Id { get; set; }
   }

   public class MongoUserEntity : UserEntity
   {
      public ObjectId Id { get; set; }
   }
}