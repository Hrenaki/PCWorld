using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchZone.Filters.Parsers
{
   public interface IFilterParser<T>
   {
      public string Parse(T filter);
      public bool CanParse(T filter);
   }

   public interface IProductFilterParser : IFilterParser<IProductFilter>
   { }

   public interface ICategoryFilterParser : IFilterParser<ICategoryFilter>
   { }
}