using Core.Exceptions;
using Data.Common;
using Data.Common.Products;
using Data.EntityFramework;
using Data.EntityFramework.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserZone
{
    public interface IBasketService
   {
      public void AddProduct(UserEntity user, ProductEntity product, int quantity);
      public void RemoveProduct(UserEntity user, BasketItemEntity basketItem);
      public IEnumerable<BasketItemEntity> GetBasketItems(UserEntity user);
   }

   internal class EfBasketService : IBasketService
   {
      private readonly MainDbContext dbContext;

      public EfBasketService(MainDbContext dbContext)
      {
         this.dbContext = dbContext;
      }

      public void AddProduct(UserEntity user, ProductEntity product, int quantity)
      {
         var userEntity = ConvertEntity<UserEntity, EfUserEntity>(user);
         var productEntity = ConvertEntity<ProductEntity, EfProductEntity>(product);

         using (var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               var userProduct = dbContext.UserProducts.FirstOrDefault(up => up.UserId == userEntity.Id && up.ProductId == productEntity.Id);
               if (userProduct != null)
               {
                  userProduct.Quantity += quantity;

                  dbContext.SaveChanges();
                  transaction.Commit();
               }

               userProduct = new EfBasketItemEntity()
               {
                  UserId = userEntity.Id,
                  ProductId = productEntity.Id,
                  Quantity = quantity,
                  User = user,
                  Product = product
               };

               dbContext.UserProducts.Add(userProduct);
               dbContext.SaveChanges();
               transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();
            }
         }
      }

      public IEnumerable<BasketItemEntity> GetBasketItems(UserEntity user)
      {
         var userEntity = ConvertEntity<UserEntity, EfUserEntity>(user);
         return dbContext.UserProducts.Where(up => up.UserId == userEntity.Id);
      }

      public void RemoveProduct(UserEntity user, BasketItemEntity basketItem)
      {
         var userEntity = ConvertEntity<UserEntity, EfUserEntity>(user);
         var basketItemEntity = ConvertEntity<BasketItemEntity, EfBasketItemEntity>(basketItem);

         if (userEntity.Id != basketItemEntity.UserId)
            return;

         using (var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               dbContext.UserProducts.Remove(basketItemEntity);
               dbContext.SaveChanges();
               transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();
            }
         }
      }

      private TDest ConvertEntity<TSource, TDest>(TSource entity) where TSource : class where TDest : class
      {
         return entity as TDest ?? 
            throw new EntityWrongTypeException(nameof(TSource), nameof(TDest));
      }
   }
}