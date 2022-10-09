using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
	public struct SearchResponseItem<T>
	{
		public string Name { get; set; }
		public int Price { get; set; }
		public string ShopName { get; set; }
		public T AdditionalInformation { get; set; }
	}

	public struct SearchResponse<T>
	{
		public List<T> Items { get; set; }
	}
}
