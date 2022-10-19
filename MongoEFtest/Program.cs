using MongoDB.Bson;
using MongoDB.Driver;
using MongoEFtest;
using MongoEFtest.Mongo;

namespace Test
{
   class Program
   {
      public static void Main(string[] args)
      {
         MongoClient client = new MongoClient("mongodb://127.0.0.1");
         IMongoDatabase database = client.GetDatabase("pcworld");

         IMongoCollection<MongoUserEntity> users = database.GetCollection<MongoUserEntity>("users");
         var result = users.Find(user => user.Name == "hrenaki").ToList().First();

         Console.ReadLine();
      }
   }
}
