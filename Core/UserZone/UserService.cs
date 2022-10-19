using Core.Configuration;
using Data;
using Data.Common;
using Data.EntityFramework;
using Data.Mongodb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Core.UserZone
{
   public interface IUserService
   {
      public bool TryAddUser(string username, string passwordHash, string email, out UserEntity? userEntity, string userRole = "customer");
      public bool TryGetUserByName(string username, out UserEntity? userEntity);
      public bool TryUpdateUser(UserEntity oldUserEntity, UserEntity newUserEntity);
      public bool TryDeleteUser(string username);
   }

   internal class EfUserService : IUserService
   {
      private readonly MainDbContext dbContext;

      public EfUserService(MainDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      public bool TryAddUser(string username,
                             string passwordHash,
                             string email,
                             out UserEntity? userEntity, 
                             string userRole = "customer")
      {
         userEntity = null;

         using (var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               if (dbContext.Users.Any(u => u.Name == username))
                  return false;

               userEntity = new EfUserEntity()
               {
                  Name = username,
                  PasswordHash = passwordHash,
                  Email = email,
                  UserRoleEntity = dbContext.UserRoles.First(role => role.Name == "customer")
               };

               dbContext.Users.Add((userEntity as EfUserEntity)!);
               dbContext.SaveChanges();

               transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();
               return false;
            }
         }

         return true;
      }

      // Returns User from DB with inputted username if exists
      public bool TryGetUserByName(string username, out UserEntity? userEntity)
      {
         userEntity = dbContext.Users.Include(u => u.UserRoleEntity).FirstOrDefault(u => u.Name == username);
         return userEntity != null;
      }

      // Returns 'true' if replacement is successful, else 'false'
      public bool TryUpdateUser(UserEntity oldUserEntity, UserEntity newUserEntity)
      {
         return false;
      }

      public bool TryDeleteUser(string username)
      {
         using (var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               var user = dbContext.Users.FirstOrDefault(u => u.Name == username);
               if (user != null)
                  dbContext.Users.Remove(user);
               dbContext.SaveChanges();

               transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();
               return false;
            }
         }

         return true;
      }
   }

   internal class MongoUserService : IUserService
   {
      private IMongoCollection<MongoUserEntity> users;
      private IUserRoleService userRoleService;

      public MongoUserService(MongoClient mongoClient, 
                              IOptions<MongoDBSettings> settings, 
                              IUserRoleService userRoleService)
      {
         ArgumentNullException.ThrowIfNull(mongoClient, nameof(mongoClient));
         ArgumentNullException.ThrowIfNull(settings, nameof(settings));
         ArgumentNullException.ThrowIfNull(userRoleService, nameof(userRoleService));

         IMongoDatabase database = mongoClient.GetDatabase(settings.Value.DatabaseName) ??
            throw new ArgumentNullException(settings.Value.DatabaseName);

         users = database.GetCollection<MongoUserEntity>(settings.Value.UserCollectionName) ??
            throw new ArgumentNullException(settings.Value.UserCollectionName);

         this.userRoleService = userRoleService;
      }

      public bool TryAddUser(string username, string passwordHash, string email, 
                             out UserEntity? userEntity, string userRole = "customer")
      {
         userEntity = null;

         try
         {
            var userRoleEntity = userRoleService.GetUserRole(userRole);
            if (userRoleEntity == null)
               return false;

            var mongoUserEntity = new MongoUserEntity()
            {
               Name = username,
               PasswordHash = passwordHash,
               Email = email,
               UserRoleEntity = userRoleEntity
            };

            users.InsertOne(mongoUserEntity);

            userEntity = mongoUserEntity;
            return true;
         }
         catch(Exception)
         {
            userEntity = null;
            return false;
         }
      }

      public bool TryDeleteUser(string username)
      {
         try
         {
            users.FindOneAndDelete(u => u.Name == username);
            return true;
         }
         catch(Exception)
         {
            return false;
         }
      }

      public bool TryGetUserByName(string username, out UserEntity? userEntity)
      {
         userEntity = users.Find(u => u.Name == username).FirstOrDefault();
         return userEntity != null;
      }

      public bool TryUpdateUser(UserEntity oldUserEntity, UserEntity newUserEntity)
      {
         throw new NotImplementedException();
      }
   }
}