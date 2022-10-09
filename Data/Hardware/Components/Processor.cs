using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Hardware.Components
{
	internal class ProcessorSocket : ProductEntity
	{ }

	internal class Processor : ProductEntity
	{
		public ProcessorSocket Socket { get; set; }
		public Manufacturer Manufacturer { get; set; }
		public ushort CoreCount{ get; set; }
		public ushort ThreadCount{ get; set; }
		public ushort StrongCoreCount{ get; set; }
		public ushort EnergyEfficientCoreCount{ get; set; }
		public bool GraphicCoreAvaliable { get; set; }
		public uint ReleaseYear{ get; set; }
	}
}
