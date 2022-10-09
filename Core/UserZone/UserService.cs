using Data;
using Microsoft.EntityFrameworkCore;

namespace Core.UserZone
{
   public interface IUserService
   {
      public bool TryAddUser(string username, string passwordHash, string email, out User? user);
      public bool TryGetUserByName(string username, out User? user);
      public bool TryUpdateUser(User oldUser, User newUser);
      public bool TryDeleteUser(string username);
   }

   public static class UserServiceFactory
   {
      public static IUserService CreateDefault(MainDbContext dbContext, IBasketService basketService)
      {
         return new UserService(dbContext, basketService);
      }
   }

   internal class UserService : IUserService
   {
      private readonly MainDbContext dbContext;
      private readonly IBasketService basketService;

      public UserService(MainDbContext dbContext, IBasketService basketService)
      {
         this.dbContext = dbContext;
         this.basketService = basketService;
      }

      public bool TryAddUser(string username,
                             string passwordHash,
                             string email,
                             out User? user)
      {
         user = null;

         using (var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               if (dbContext.Users.Any(u => u.Name == username))
                  return false;

               var userEntity = new UserEntity()
               {
                  Name = username,
                  PasswordHash = passwordHash,
                  Email = email,
                  UserRole = dbContext.UserRoles.First(role => role.Name == UserRole.Customer.ToString())
               };

               dbContext.Users.Add(userEntity);
               dbContext.SaveChanges();

               user = new User(userEntity, basketService);
               FillUserBasket(user);

               transaction.Commit();
            }
            catch(Exception)
            {
               transaction.Rollback();
               return false;
            }
         }

         return true;
      }

      // Returns User from DB with inputted username if exists
      public bool TryGetUserByName(string username, out User? user)
      {
         var userEntity = dbContext.Users.Include(u => u.UserRole).FirstOrDefault(u => u.Name == username);
         
         if (userEntity == null)
         {
            user = null;
            return false;
         }

         user = new User(userEntity, basketService);
         FillUserBasket(user);
         return true;
      }

      // Returns 'true' if replacement is successful, else 'false'
      public bool TryUpdateUser(User oldUser, User newUser)
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

      private void FillUserBasket(User user)
      {
         int userId = user.Entity.Id;
         var basketItems = dbContext.UserProducts.Where(bItem => bItem.UserId == userId)
                                                 .Include(bItem => bItem.Product)
                                                   .ThenInclude(p => p.Category)
                                                 .Include(bItem => bItem.User)
                                                 .Select(bItem => new BasketItem(bItem));
         user.Basket.Fill(basketItems);
      }
   }
}