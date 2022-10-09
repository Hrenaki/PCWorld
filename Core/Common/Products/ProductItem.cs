using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Products
{
	public enum ProductItemType
	{
		Motherboard,
		Processor,
		Graphicscard,

	}

	public struct ProductItem
	{
		public string Name { get; init; }
		public int Price { get; init; }

		public string Shop { get; init; }
		public bool Exists { get; init; }

		public ICategoryInfo AdditionalInfo { get; init; }
	}
}
