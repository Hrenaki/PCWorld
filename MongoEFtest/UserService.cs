using MongoDB.Bson;
using MongoEFtest.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoEFtest
{
   interface IUserService<T>
   {
      public T GetUser(string name);
   }

   public class MongoUserService : IUserService<MongoUserEntity>
   {
      public MongoUserEntity GetUser(string name)
      {
         throw new NotImplementedException();
      }
   }
}