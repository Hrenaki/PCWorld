using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Core.UserZone
{
   public enum UserRole
   {
      Customer,
      Personel,
      Admin,
      Default = Customer
   }

   public class User
   {
      internal UserEntity Entity { get; init; }

      public string Name => Entity.Name;
      public string Email => Entity.Email;
      public string PasswordHash => Entity.PasswordHash;
      public UserRole Role => Enum.Parse<UserRole>(Entity.UserRole.Name);

      public Basket Basket { get; private set; }

      public User(UserEntity entity, IBasketService basketService)
      {
         Entity = entity;
         Basket = new Basket(entity, basketService);
      }
   }
}