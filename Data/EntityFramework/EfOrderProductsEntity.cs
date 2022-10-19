using Data.EntityFramework.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework
{
    public class EfOrderProductEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public EfProductEntity Product { get; set; }

        public int Quantity { get; set; }
    }
}