using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserZone
{
   public class BasketItem
   {
      internal BasketItemEntity Entity { get; init; }

      public string ProductName => Entity.Product.Name; 
      public decimal ProductPrice => Entity.Product.Price;
      public int Quantity => Entity.Quantity;

      public BasketItem(BasketItemEntity entity)
      {
         Entity = entity;
      }
   }

   public class Basket
   {
      private List<BasketItem> items = new List<BasketItem>();
      public IReadOnlyList<BasketItem> Items
      {
         get 
         {
            Update();
            return items.AsReadOnly();
         }
      }

      internal UserEntity Owner { get; init; }
      private IBasketService basketService;

      public Basket(UserEntity owner, IBasketService basketService)
      {
         Owner = owner;
         this.basketService = basketService;
      }

      public void AddProduct(Product product, int quantity = 1)
      {
         basketService.AddProduct(Owner, product.Entity, quantity);
      }

      public void RemoveProduct(BasketItem basketItem)
      {
         basketService.RemoveProduct(Owner, basketItem.Entity);
      }

      public void Update()
      {
         items.Clear();
         items.AddRange(basketService.GetBasketItems(Owner).Select(e => new BasketItem(e)));
      }

      internal void Fill(IEnumerable<BasketItem> items)
      {
         if (items.Any())
            return;

         this.items.AddRange(items);
      }
   }
}