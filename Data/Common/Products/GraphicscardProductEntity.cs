using Data.Mongodb.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common.Products
{
   public class GraphicscardProductEntity : ProductEntity
   {
      public GraphicscardSpecifications Specifications { get; set; }
   }

   public class GraphicscardSpecifications
   {
      public string GraphicsProcessor { get; set; }
      public int VRAM { get; set; }
      public string VRAMType { get; set; }
   }
}