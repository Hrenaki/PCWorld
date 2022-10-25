using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Common.Products;

namespace Data.Common
{
    public class BasketItemEntity
	{
		public UserEntity User { get; set; }
		public ProductEntity Product { get; set; }
		
		public PriceInfo PriceInfo { get; set; }

		public int Quantity { get; set; }
	}
}