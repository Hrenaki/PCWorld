using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public class OrderEntity : Entity
   {
      public string Hash { get; set; }
      
      public int UserId { get; set; }
      [ForeignKey(nameof(UserId))]
      public UserEntity User { get; set; }

      public int StatusId { get; set; }
      [ForeignKey(nameof(StatusId))]
      public OrderStatusEntity Status { get; set; }
   }
}