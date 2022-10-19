using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework.Products
{
   public class EfGraphicscardSpecificationEntity
   {
      public int ProductId { get; set; }
      public string GraphicsProcessor { get; set; }
      public int VRAM { get; set; }
      public string VRAMType { get; set; }
   }
}