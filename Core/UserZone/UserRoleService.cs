using Core.Configuration;
using Data.Common;
using Data.Mongodb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.UserZone
{
   public interface IUserRoleService
   {
      public UserRoleEntity? GetUserRole(string name);
   }

   internal class MongoUserRoleService : IUserRoleService
   {
      private IMongoCollection<MongoUserRoleEntity> userRoles;

      public MongoUserRoleService(MongoClient client, IOptions<MongoDBSettings> settings)
      {
         ArgumentNullException.ThrowIfNull(client, nameof(client));
         ArgumentNullException.ThrowIfNull(settings, nameof(settings));
         ArgumentNullException.ThrowIfNull(settings.Value, nameof(settings.Value));
         ArgumentNullException.ThrowIfNull(settings.Value.DatabaseName, nameof(settings.Value.DatabaseName));
         ArgumentNullException.ThrowIfNull(settings.Value.UserRoleCollectionName, nameof(settings.Value.UserRoleCollectionName));

         IMongoDatabase database = client.GetDatabase(settings.Value.DatabaseName) ??
            throw new ArgumentNullException(nameof(database));

         userRoles = database.GetCollection<MongoUserRoleEntity>(settings.Value.UserRoleCollectionName) ??
            throw new ArgumentNullException(nameof(userRoles));
      }

      public UserRoleEntity? GetUserRole(string name)
      {
         return userRoles.Find(new BsonDocument { { "Name", new Regex($"^{name}$", RegexOptions.IgnoreCase) } }).FirstOrDefault();
      }
   }
}