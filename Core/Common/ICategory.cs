using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
	public interface ICategoryInfo
	{
		public string Name { get; }

		public Dictionary<string, string> ToDictionary();
	}

	internal class ProcessorCategory : ICategoryInfo
	{
		public string Name { get; } = "Processor";

		public Dictionary<string, string> ToDictionary()
		{
			throw new NotImplementedException();
		}
	}

	internal class GraphicsCardCategory : ICategoryInfo
	{
		public string Name { get; } = "Graphicscard";

		public Dictionary<string, string> ToDictionary()
		{
			throw new NotImplementedException();
		}
	}
}