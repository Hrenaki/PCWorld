using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongodb.Products
{
   public class MongoGraphicscardProductEntity : MongoProductEntity
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
