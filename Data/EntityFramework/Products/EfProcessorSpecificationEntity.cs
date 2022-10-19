using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework.Products
{
   public class EfProcessorSpecificationEntity
   {
      public int ProductId { get; set; }
      [ForeignKey(nameof(ProductId))]
      public EfProductEntity ProductEntity { get; set; }

      public string Socket { get; set; }
      public int CoreCount { get; set; }
      public int ThreadCount { get; set; }
   }
}