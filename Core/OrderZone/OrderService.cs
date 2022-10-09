using Core.UserZone;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.OrderZone
{
   public interface IOrderService
   {
      public Order? CreateOrder(Basket basket);
      public List<Order> GetAllUserOrders(User user);
   }

   internal class OrderService : IOrderService
   {
      private readonly MainDbContext dbContext;
      private readonly IHashService hashService;

      public OrderService(MainDbContext dbContext, IHashService hashService)
      {
         this.dbContext = dbContext;
         this.hashService = hashService;
      }

      public Order? CreateOrder(Basket basket)
      {
         using(var transaction = dbContext.Database.BeginTransaction())
         {
            try
            {
               var firstBasketItem = basket.Items.First();
               var statusEntity = dbContext.OrderStatuses.FirstOrDefault(st => st.Name == OrderStatus.Created.ToString());

               var orderEntity = new OrderEntity()
               {
                  Hash = hashService.GetStringHash(hashService.GetStringHash(firstBasketItem.ProductName, DateTime.Now.ToString()), basket.Owner.Name),
                  UserId = basket.Owner.Id,
                  User = basket.Owner,
                  StatusId = statusEntity.Id,
                  Status = statusEntity
               };

               dbContext.Orders.Add(orderEntity);
               dbContext.SaveChanges();

               OrderProductEntity currentOrderProduct;
               List<OrderProductEntity> orderProducts = new List<OrderProductEntity>();
               foreach(var basketItem in basket.Items)
               {
                  currentOrderProduct = new OrderProductEntity()
                  {
                     OrderId = orderEntity.Id,
                     ProductId = basketItem.Entity.ProductId,
                     Quantity = basketItem.Quantity
                  };

                  dbContext.OrderProducts.Add(currentOrderProduct);
               }
               dbContext.SaveChanges();

               transaction.Commit();

               var order = new Order(orderEntity, orderProducts.Select(op => new OrderProduct(op)));
               return order;
            }
            catch(Exception)
            {
               transaction.Rollback();
               return null;
            }
         }
      }

      public List<Order> GetAllUserOrders(User user)
      {
         var orderEntities = dbContext.Orders.Where(oe => oe.UserId == user.Entity.Id)
                                             .Include(oe => oe.Status).AsEnumerable();
         var orders = new List<Order>();
         foreach(var orderEntity in orderEntities)
         {
            orderEntity.User = user.Entity;

            var orderProducts = dbContext.OrderProducts.Where(po => po.OrderId == orderEntity.Id)
                                                              .Include(po => po.Product)
                                                              .Select(po => new OrderProduct(po));
            orders.Add(new Order(orderEntity, orderProducts));
         }

         return orders;
      }
   }
}