using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class ProductEntity : Entity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }

		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public CategoryEntity Category { get; set; }
	}
}