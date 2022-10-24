using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.Parsers
{
   public interface IProductFilterParser
   {
      public string Parse(IProductFilter filter);
      public bool CanParse(IProductFilter filter);
   }
}