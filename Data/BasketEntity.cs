using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UserZone
{
	public class BasketItemEntity
	{
		public int UserId{ get; set; }
		[ForeignKey(nameof(UserId))]
		public UserEntity User { get; set; }

		public int ProductId{ get; set; }
		[ForeignKey(nameof(ProductId))]
		public ProductEntity Product { get; set; }

      public int Quantity { get; set; }

		public bool Equals(BasketItemEntity other)
		{
			return UserId == other.UserId && ProductId == other.ProductId && Quantity == other.Quantity;
		}
	}
}