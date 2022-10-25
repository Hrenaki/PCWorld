using Data.Common;
using Data.Common.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework.Products
{
   public class EfProductEntity : ProductEntity
   {
      public int Id { get; set; }
   }
}