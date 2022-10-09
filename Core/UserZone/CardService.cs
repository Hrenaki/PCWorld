using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserZone
{
   public class Card
   {
      public string Number { get; set; }
      public string HolderName { get; set; }
      public int CVC { get; set; }
   }

   //public interface ICardService
   //{
   //   public Result ExecutePaymentOperation(Card card, decimal money);
   //}
   //
   //public static class CardServiceFactory
   //{
   //   public static ICardService CreateDefault()
   //   {
   //      return new CardService();
   //   }
   //}
   //
   //internal class CardService : ICardService
   //{
   //   public Result ExecutePaymentOperation(Card card, decimal money)
   //   {
   //      return new Result() { Successful = true, Message = "Ok" };
   //   }
   //}
}