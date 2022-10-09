using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
	internal struct SearchFilter<T>
	{
		public int MinPrice { get; set; }
		public int MaxPrice { get; set; }

		public bool Exists { get; set; }

		public T CategoryFilter { get; set; }
	}

	internal struct SearchRequest<T>
	{
		public string SearchString { get; set; }
		public SearchFilter<T>? Filter { get; set; }
	}
}