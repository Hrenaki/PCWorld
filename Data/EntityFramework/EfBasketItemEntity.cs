using Data.Common;
using Data.EntityFramework.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework
{
    public class EfBasketItemEntity : BasketItemEntity
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int ShopProductId { get; set; }

        public bool Equals(EfBasketItemEntity other)
        {
            return UserId == other.UserId && ProductId == other.ProductId && Quantity == other.Quantity;
        }
    }
}