using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public class OrderProductEntity
   {
      public int OrderId { get; set; }

      public int ProductId { get; set; }
      [ForeignKey(nameof(ProductId))]
      public ProductEntity Product { get; set; }

      public int Quantity { get; set; }
   }
}