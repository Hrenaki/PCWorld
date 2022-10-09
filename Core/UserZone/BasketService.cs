using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserZone
{
   public interface IBasketService
   {
      public BasketItemEntity? AddProduct(UserEntity user, ProductEntity product, int quantity);
      public void RemoveProduct(UserEntity user, BasketItemEntity basketItem);
      public IEnumerable<BasketItemEntity> GetBasketItems(UserEntity user);
   }

   public static class BasketServiceFactory
   {
      public static IBasketService CreateDefault(MainDbContext dbContext)
      {
         return new BasketService(dbContext);
      }
   }

   internal class BasketService : IBasketService
   {
      private readonly MainDbContext dbContext;

      public BasketService(MainDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      public BasketItemEntity? AddProduct(UserEntity user, ProductEntity product, int quantity)
      {
         using(var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               var userProduct = dbContext.UserProducts.FirstOrDefault(up => up.UserId == user.Id && up.ProductId == product.Id);
               if(userProduct != null)
               {
                  userProduct.Quantity += quantity;

                  dbContext.SaveChanges();
                  transaction.Commit();

                  return userProduct;
               }

               userProduct = new BasketItemEntity()
               {
                  UserId = user.Id,
                  ProductId = product.Id,
                  Quantity = quantity,
                  User = user,
                  Product = product
               };

               dbContext.UserProducts.Add(userProduct);
               dbContext.SaveChanges();
               transaction.Commit();

               return userProduct;
            }
            catch(Exception)
            {
               transaction.Rollback();
               return null;
            }
         }
      }

      public IEnumerable<BasketItemEntity> GetBasketItems(UserEntity user)
      {
         return dbContext.UserProducts.Where(up => up.UserId == user.Id);
      }

      public void RemoveProduct(UserEntity user, BasketItemEntity basketItem)
      {
         if (user.Id != basketItem.UserId)
            return;

         using(var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               dbContext.UserProducts.Remove(basketItem);
               dbContext.SaveChanges();
               transaction.Commit();
            }
            catch(Exception)
            {
               transaction.Rollback();
            }
         }
      }
   }
}
